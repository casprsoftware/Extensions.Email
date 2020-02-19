using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CASPR.Extensions.Email
{
    public class QueueEmailService : IHostedService, IDisposable
    {
        public const int TimePeriodInSeconds = 60;
        
        private Timer _timer;
        private readonly ILogger<QueueEmailService> _logger;
        private readonly IEmailMessageQueue _emailMessageQueue;
        private readonly IEmailSender _emailSender;
        private bool _timerEnabled = true;

        public QueueEmailService(
            ILogger<QueueEmailService> logger, 
            IEmailSender emailSender, 
            IEmailMessageQueue emailMessageQueue)
        {
            _logger = logger;
            _emailSender = emailSender;
            _emailMessageQueue = emailMessageQueue;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting Queue Email Service");
            _timer = new Timer(SendEmails, null, TimeSpan.Zero, TimeSpan.FromSeconds(TimePeriodInSeconds));

            _logger.LogInformation("Started Queue Email Service");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stopping Queue Email Service");
            _timer?.Change(Timeout.Infinite, 0);
            _timerEnabled = false;
            _logger.LogInformation("Stopped Queue Email Service");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async void SendEmails(object state)
        {
            var msg = await _emailMessageQueue.NextAsync();
            while (msg!=null)
            {

                if (_timerEnabled)
                {
                    _timer?.Change(Timeout.Infinite, 0);
                    _timerEnabled = false;
                    _logger.LogDebug("Timer stopped");
                }

                try
                {
                    _logger.LogDebug("Sending the email '{Subject}'", msg.Subject);
                    await _emailSender.SendAsync(msg);
                    _logger.LogInformation("Sent the email '{Subject}'", msg.Subject);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "An error occured during sending email.");
                }

                msg = await _emailMessageQueue.NextAsync();
            }

            if (!_timerEnabled)
            {
                _timer?.Change(TimeSpan.Zero, TimeSpan.FromSeconds(TimePeriodInSeconds));
                _timerEnabled = true;
                _logger.LogDebug("Timer started");
            }
        }
    }
}