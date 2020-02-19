using System.Threading;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email.Senders
{
    public class NullEmailSender : IEmailSender
    {
        public EmailResponse Send(EmailMessage email, CancellationToken? token = null)
        {
            return new EmailResponse();
        }

        public Task<EmailResponse> SendAsync(EmailMessage email, CancellationToken? token = null)
        {
            return Task.FromResult(Send(email, token));
        }
    }
}
