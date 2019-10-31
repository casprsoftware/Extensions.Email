using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;
using Microsoft.Extensions.Options;

namespace CASPR.Extensions.Email.Senders
{
    /// <summary>
    /// Save email to directory,
    /// Email Sender implementation
    /// </summary>
    public class SaveToDiskEmailSender : IEmailSender
    {
        private readonly SaveToDiskEmailSenderOptions _options;

        public SaveToDiskEmailSender(IOptions<SaveToDiskEmailSenderOptions> optionsAccessor)
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
            var filename = $"{_options.Directory.TrimEnd('\\')}\\{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
            using (var sw = new StreamWriter(File.OpenWrite(filename)))
            {
                sw.WriteLine($"From: {email.FromAddress}");
                sw.WriteLine($"To: {string.Join(",", email.ToAddresses)}");
                sw.WriteLine($"Cc: {string.Join(",", email.CcAddresses)}");
                sw.WriteLine($"Bcc: {string.Join(",", email.BccAddresses)}");
                sw.WriteLine($"ReplyTo: {email.ReplyToAddress}");
                sw.WriteLine($"Subject: {email.Subject}");
                
                sw.WriteLine();
                await sw.WriteAsync(email.Body);
            }

            return response;
        }
    }
}
