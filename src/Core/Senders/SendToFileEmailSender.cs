using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;
using Microsoft.Extensions.Options;

namespace CASPR.Extensions.Email.Senders
{
    /// <summary>
    /// Save an email to file,
    /// Email Sender implementation
    /// </summary>
    public class SendToFileEmailSender : IEmailSender
    {
        private readonly SendToFileEmailSenderOptions _options;

        public SendToFileEmailSender(IOptions<SendToFileEmailSenderOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public EmailResponse Send(EmailMessage email, CancellationToken? token = null)
        {
            return SendAsync(email, token).GetAwaiter().GetResult();
        }

        public async Task<EmailResponse> SendAsync(EmailMessage email, CancellationToken? token = null)
        {
            var response = new EmailResponse();
            var filename = $"{_options.Directory.TrimEnd('\\')}\\email_{DateTime.Now:yyyyMMddHHmmss}.txt";
            using (var sw = new StreamWriter(File.OpenWrite(filename)))
            {
                sw.WriteLine($"From: {email.FromAddress}");
                sw.WriteLine($"To: {string.Join(", ", email.ToAddresses)}");
                sw.WriteLine($"Cc: {string.Join(", ", email.CcAddresses)}");
                sw.WriteLine($"Bcc: {string.Join(", ", email.BccAddresses)}");
                sw.WriteLine($"ReplyTo: {email.ReplyToAddress}");
                sw.WriteLine($"Subject: {email.Subject}");
                
                sw.WriteLine();
                await sw.WriteAsync(email.Body);
            }

            return response;
        }
    }
}
