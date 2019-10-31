using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Exceptions;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// Represents email builder.
    /// </summary>
    public class EmailBuilder
    {
        #region Private Declarations
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateEngine _templateEngine;
        private readonly EmailMessage _emailMessage;
        private readonly IEmailTemplateStorage _emailTemplateStorage;
        #endregion

        #region Constructor
        public EmailBuilder(
            IEmailSender emailSender,
            IEmailTemplateEngine templateEngine,
            IEmailTemplateStorage emailTemplateStorage,
            EmailAddress defaultFrom)
        {
            _emailSender = emailSender;
            _templateEngine = templateEngine;
            _emailTemplateStorage = emailTemplateStorage;
            _emailMessage = new EmailMessage
            {
                FromAddress = defaultFrom
            };
        }
        #endregion

        #region Add Email Address to "TO"
        /// <summary>
        /// Adds a recipient to the email.
        /// </summary>
        /// <param name="emailAddress">Email address of recipient.</param>
        /// <param name="name">Name of recipient.</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder To(string emailAddress, string name = null)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(emailAddress));
            }
            var toEmailAddress = new EmailAddress(emailAddress, name);
            if (!_emailMessage.ToAddresses.Add(toEmailAddress))
            {
                throw new EmailAddressAlreadyAddedException(emailAddress);
            }
            return this;
        }

        /// <summary>
        /// Adds all recipients in list to email.
        /// </summary>
        /// <param name="emailAddresses">List of recipients.</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder To(HashSet<EmailAddress> emailAddresses)
        {
            if (emailAddresses == null)
            {
                throw new ArgumentNullException(nameof(emailAddresses));
            }

            foreach (var emailAddress in emailAddresses)
            {
                if (!_emailMessage.ToAddresses.Add(emailAddress))
                {
                    throw new EmailAddressAlreadyAddedException(emailAddress.Value);
                }
            }

            return this;
        }
        #endregion

        #region Add Email Address to "CC"
        /// <summary>
        /// Adds a Carbon Copy to the email.
        /// </summary>
        /// <param name="emailAddress">Email address of recipient.</param>
        /// <param name="name">Name of recipient.</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder Cc(string emailAddress, string name = null)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(emailAddress));
            }
            var address = new EmailAddress(emailAddress, name);
            if (!_emailMessage.CcAddresses.Add(address))
            {
                throw new EmailAddressAlreadyAddedException(emailAddress);
            }
            return this;
        }

        /// <summary>
        /// Adds all Carbon Copy in list to an email.
        /// </summary>
        /// <param name="emailAddresses">List of recipients to CC.</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder Cc(HashSet<EmailAddress> emailAddresses)
        {
            if (emailAddresses == null)
            {
                throw new ArgumentNullException(nameof(emailAddresses));
            }

            foreach (var emailAddress in emailAddresses)
            {
                if (!_emailMessage.CcAddresses.Add(emailAddress))
                {
                    throw new EmailAddressAlreadyAddedException(emailAddress.Value);
                }
            }

            return this;
        }
        #endregion

        #region Add Email Address to "BCC"
        /// <summary>
        /// Adds a blind carbon copy to the email.
        /// </summary>
        /// <param name="emailAddress">Email address of recipient.</param>
        /// <param name="name">Name of recipient.</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder Bcc(string emailAddress, string name = null)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(emailAddress));
            }
            var address = new EmailAddress(emailAddress, name);
            if (!_emailMessage.BccAddresses.Add(address))
            {
                throw new EmailAddressAlreadyAddedException(emailAddress);
            }
            return this;
        }

        /// <summary>
        /// Adds all blind carbon copy in list to the email.
        /// </summary>
        /// <param name="emailAddresses">List of recipients.</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder Bcc(HashSet<EmailAddress> emailAddresses)
        {
            if (emailAddresses == null)
            {
                throw new ArgumentNullException(nameof(emailAddresses));
            }

            foreach (var emailAddress in emailAddresses)
            {
                if (!_emailMessage.BccAddresses.Add(emailAddress))
                {
                    throw new EmailAddressAlreadyAddedException(emailAddress.Value);
                }
            }

            return this;
        }
        #endregion

        #region Set Priority

        public EmailBuilder HighPriority()
        {
            _emailMessage.Priority = EmailPriority.High;
            return this;
        }

        public EmailBuilder LowPriority()
        {
            _emailMessage.Priority = EmailPriority.Low;
            return this;
        }

        #endregion

        /// <summary>
        /// Sets the ReplyTo on the email. 
        /// </summary>
        /// <param name="emailAddress">The ReplyTo email address.</param>
        /// <param name="name">Name of the ReplyTo.</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder ReplyTo(string emailAddress, string name = null)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(emailAddress));
            }

            _emailMessage.ReplyToAddress = new EmailAddress(emailAddress, name);

            return this;
        }

        /// <summary>
        /// Sets a subject of the email.
        /// </summary>
        /// <param name="subject">Content of the subject</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder Subject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(subject));
            }

            _emailMessage.Subject = subject;
            return this;
        }

        /// <summary>
        /// Adds a body to the email.
        /// </summary>
        /// <param name="body">The content of the body.</param>
        /// <param name="isHtml">True if body is HTML, false for plain text (optional).</param>
        /// <returns>Current instance of the <see cref="EmailBuilder"/>.</returns>
        public EmailBuilder Body(string body, bool isHtml = false)
        {
            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(body));
            }

            _emailMessage.Body = body;
            _emailMessage.IsHtml = isHtml;
            return this;
        }

        /// <summary>
        /// Using a template for set subject and body
        /// </summary>
        /// <typeparam name="TModel">The model</typeparam>
        /// <param name="templateName">The template name</param>
        /// <param name="model">The model object</param>
        /// <param name="culture"></param>
        /// <exception cref="EmailTemplateNotFoundException"></exception>
        /// <returns></returns>
        public EmailBuilder UsingTemplate<TModel>(string templateName, TModel model, CultureInfo culture = null)
        {
            var template = _emailTemplateStorage
                .GetTemplateAsync(templateName, culture)
                .GetAwaiter()
                .GetResult();

            if (template == null)
            {
                throw new EmailTemplateNotFoundException();
            }

            if (!string.IsNullOrEmpty(template.Subject))
            {
                _emailMessage.Subject = _templateEngine.RenderAsync(
                    templateKey: $"subject_{templateName}",
                    templateContent: template.Subject,
                    model: model,
                    isHtml: false
                    ).GetAwaiter().GetResult();
            }
            _emailMessage.Body = _templateEngine.RenderAsync(
                templateKey: $"body_{templateName}",
                templateContent: template.Body,
                model: model,
                isHtml: true
                ).GetAwaiter().GetResult();

            return this;
        }

        public EmailBuilder Attach(EmailAttachment attachment)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException(nameof(attachment));
            }

            _emailMessage.Attachments.Add(attachment);
            return this;
        }

        #region Send Message
        /// <summary>
        /// Sends the email.
        /// </summary>
        public void Send()
        {
            SendAsync()
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="token">Cancellation token for the task.</param>
        /// <returns>The sending task, <see cref="Task"/>.</returns>
        public async Task SendAsync(CancellationToken? token = null)
        {
            await _emailSender.SendAsync(_emailMessage, token);
        }
        #endregion
    }
}
