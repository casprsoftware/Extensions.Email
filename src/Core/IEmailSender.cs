using System.Threading;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// Email sender interface.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Send the email message.
        /// </summary>
        /// <param name="email">The email message. See <see cref="EmailMessage"/>.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        EmailResponse Send(EmailMessage email, CancellationToken? token = null);

        /// <summary>
        /// Send the email message.
        /// </summary>
        /// <param name="email">The email message. See <see cref="EmailMessage"/>.</param>
        /// <param name="token">The <see cref="CancellationToken"/>.</param>
        /// <returns></returns>
        Task<EmailResponse> SendAsync(EmailMessage email, CancellationToken? token = null);
    }
}
