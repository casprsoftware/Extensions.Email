using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using EmailAddress = CASPR.Extensions.Email.Models.EmailAddress;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// SendGrid sender implementation 
    /// </summary>
    public sealed class SendGridEmailSender : IEmailSender
    {
        #region Constructors

        private readonly SendGridOptions _options;
        private readonly ILogger<SendGridEmailSender> _logger;

        public SendGridEmailSender(
            IOptions<SendGridOptions> optionsAccessor,
            ILogger<SendGridEmailSender> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        #endregion

        #region ISender Members

        public EmailResponse Send(EmailMessage email, CancellationToken? token = null)
        {
            return SendAsync(email, token).GetAwaiter().GetResult();
        }

        public async Task<EmailResponse> SendAsync(EmailMessage email, CancellationToken? token = null)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Send an email {Subject}", email.Subject);
            }
            // init client
            var sendGridClient = new SendGridClient(_options.ApiKey);

            // build a message
            var mailMessage = new SendGridMessage();
            mailMessage.SetSandBoxMode(_options.SandBoxMode);
            // set FROM address
            mailMessage.SetFrom(ConvertAddress(email.FromAddress));
            // add TO addresses
            mailMessage.AddTos(email.ToAddresses.Select(ConvertAddress).ToList());

            // add CC addresses
            if (email.CcAddresses.Any())
            {
                mailMessage.AddCcs(email.CcAddresses.Select(ConvertAddress).ToList());
            }

            // add BCC addresses
            if (email.BccAddresses.Any())
            {
                mailMessage.AddBccs(email.BccAddresses.Select(ConvertAddress).ToList());
            }
            
            // set reply to
            if (email.ReplyToAddress!=null)
            {
                mailMessage.ReplyTo = ConvertAddress(email.ReplyToAddress);
            }

            // set subject
            mailMessage.SetSubject(email.Subject);

            // set body
            if (email.IsHtml)
            {
                mailMessage.HtmlContent = email.Body;
            }
            else
            {
                mailMessage.PlainTextContent = email.Body;
            }

            // set priority
            switch (email.Priority)
            {
                case EmailPriority.High:
                    // https://stackoverflow.com/questions/23230250/set-email-priority-with-sendgrid-api
                    mailMessage.AddHeader("Priority", "Urgent");
                    mailMessage.AddHeader("Importance", "High");
                    // https://docs.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxcmail/2bb19f1b-b35e-4966-b1cb-1afd044e83ab
                    mailMessage.AddHeader("X-Priority", "1");
                    mailMessage.AddHeader("X-MSMail-Priority", "High");
                    break;

                case EmailPriority.Normal:
                    // Do not set anything.
                    // Leave default values. It means Normal Priority.
                    break;

                case EmailPriority.Low:
                    // https://stackoverflow.com/questions/23230250/set-email-priority-with-sendgrid-api
                    mailMessage.AddHeader("Priority", "Non-Urgent");
                    mailMessage.AddHeader("Importance", "Low");
                    // https://docs.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxcmail/2bb19f1b-b35e-4966-b1cb-1afd044e83ab
                    mailMessage.AddHeader("X-Priority", "5");
                    mailMessage.AddHeader("X-MSMail-Priority", "Low");
                    break;
            }

            // add attachments
            foreach (var attachment in email.Attachments)
            {
                var content = await GetAttachmentBase64String(attachment.Data);
                mailMessage.AddAttachment(
                    filename: attachment.FileName, 
                    base64Content: content,
                    type: attachment.ContentType);
            }

            // send email
            var sendGridResponse = await sendGridClient.SendEmailAsync(mailMessage, token.GetValueOrDefault());

            // get response
            // - success
            if (IsHttpSuccess((int)sendGridResponse.StatusCode))
            {
                return new EmailResponse();
            }
            // - errors
            var errorList = new List<string> {$"{sendGridResponse.StatusCode}"};
            var messageBodyDictionary = await sendGridResponse.DeserializeResponseBodyAsync(sendGridResponse.Body);

            if (messageBodyDictionary.ContainsKey("errors"))
            {
                var errors = messageBodyDictionary["errors"];
                foreach (var error in errors)
                {
                    errorList.Add($"{error}");
                }
            }
            return new EmailResponse(errorList);
        }

        #endregion

        #region Private Methods

        private SendGrid.Helpers.Mail.EmailAddress ConvertAddress(EmailAddress address)
        {
            return new SendGrid.Helpers.Mail.EmailAddress(address.Value, address.Name);
        }

        private bool IsHttpSuccess(int statusCode)
        {
            return statusCode >= 200 && statusCode < 300;
        }
        
        private async Task<string> GetAttachmentBase64String(Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                stream.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        #endregion
    }
}