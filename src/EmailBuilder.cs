using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Exceptions;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// The email builder
    /// </summary>
    public abstract class EmailBuilder
    {
        #region Private Declarations
        private readonly IEmailTemplateEngine _templateEngine;
        private readonly IEmailTemplateStorage _emailTemplateStorage;
        #endregion

        public EmailBuilder(
            IEmailTemplateEngine templateEngine, 
            IEmailTemplateStorage emailTemplateStorage,
            EmailAddress defaultFrom)
        {
            _templateEngine = templateEngine;
            _emailTemplateStorage = emailTemplateStorage;
            Message = new EmailMessage
            {
                FromAddress = defaultFrom
            };
        }

        protected EmailMessage Message { get; }

        /// <summary>
        /// Set FROM email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public virtual EmailBuilder From(EmailAddress emailAddress)
        {
            Message.FromAddress = emailAddress;
            return this;
        }

        #region Add Email Address to "TO"

        /// <summary>
        /// Adds a recipient to the email.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public virtual EmailBuilder To(EmailAddress emailAddress)
        {
            if (emailAddress==null)
            {
                throw new ArgumentException("Email address cannot be null.", nameof(emailAddress));
            }
            if (!Message.ToAddresses.Add(emailAddress))
            {
                throw new EmailAddressAlreadyAddedException(emailAddress.Value);
            }
            return this;
        }

        /// <summary>
        /// Adds list of recipients to the email.
        /// </summary>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public virtual EmailBuilder To(HashSet<EmailAddress> emailAddresses)
        {
            if (emailAddresses == null)
            {
                throw new ArgumentNullException(nameof(emailAddresses));
            }

            emailAddresses
                .ToList()
                .ForEach(m=> To(m));
            return this;
        }

        #endregion

        #region Add Email Address to "CC"

        /// <summary>
        /// Adds a Carbon Copy to the email.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public virtual EmailBuilder Cc(EmailAddress emailAddress)
        {
            if (emailAddress == null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (!Message.CcAddresses.Add(emailAddress))
            {
                throw new EmailAddressAlreadyAddedException(emailAddress.Value);
            }
            return this;
        }

        /// <summary>
        /// Adds a Carbon Copy to the email.
        /// </summary>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public virtual EmailBuilder Cc(HashSet<EmailAddress> emailAddresses)
        {
            if (emailAddresses == null)
            {
                throw new ArgumentNullException(nameof(emailAddresses));
            }
            emailAddresses.ToList()
                .ForEach(m=>Cc(m));
            return this;
        }

        #endregion

        #region Add Email Address to "BCC"

        /// <summary>
        /// Adds a blind carbon copy to the email.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public virtual EmailBuilder Bcc(EmailAddress emailAddress)
        {
            if (emailAddress == null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            if (!Message.BccAddresses.Add(emailAddress))
            {
                throw new EmailAddressAlreadyAddedException(emailAddress.Value);
            }

            return this;
        }

        /// <summary>
        /// Adds a blind carbon copy to the email.
        /// </summary>
        /// <param name="emailAddresses"></param>
        /// <returns></returns>
        public virtual EmailBuilder Bcc(HashSet<EmailAddress> emailAddresses)
        {
            if (emailAddresses == null)
            {
                throw new ArgumentNullException(nameof(emailAddresses));
            }

            emailAddresses.ToList()
                .ForEach(m=>Bcc(m));

            return this;
        }

        #endregion

        /// <summary>
        /// Sets the ReplyTo on the email. 
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public virtual EmailBuilder ReplyTo(EmailAddress emailAddress)
        {
            if (emailAddress == null)
            {
                throw new ArgumentNullException(nameof(emailAddress));
            }

            Message.ReplyToAddress = emailAddress;

            return this;
        }

        public virtual EmailBuilder HighPriority()
        {
            Message.Priority = EmailPriority.High;
            return this;
        }

        public virtual EmailBuilder LowPriority()
        {
            Message.Priority = EmailPriority.Low;
            return this;
        }

        public virtual EmailBuilder Subject(string subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            Message.Subject = subject;
            return this;
        }

        public virtual EmailBuilder Body(string body, bool isHtml = false)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            Message.Body = body;
            Message.IsHtml = isHtml;
            return this;
        }

        public virtual EmailBuilder Attach(EmailAttachment attachment)
        {
            if (attachment == null)
            {
                throw new ArgumentNullException(nameof(attachment));
            }

            Message.Attachments.Add(attachment);
            return this;
        }

        public virtual EmailBuilder UsingTemplate<TModel>(string templateName, TModel model, CultureInfo culture = null)
        {
            var template = _emailTemplateStorage
                .GetTemplateAsync(templateName, culture)
                .GetAwaiter()
                .GetResult();

            if (template == null)
            {
                throw new EmailTemplateNotFoundException(templateName);
            }

            if (!string.IsNullOrEmpty(template.Subject))
            {
                Message.Subject = _templateEngine.RenderAsync(
                    templateKey: $"subject_{templateName}",
                    templateContent: template.Subject,
                    model: model,
                    isHtml: false
                ).GetAwaiter().GetResult();
            }
            Message.Body = _templateEngine.RenderAsync(
                templateKey: $"body_{templateName}",
                templateContent: template.Body,
                model: model,
                isHtml: true
            ).GetAwaiter().GetResult();

            return this;
        }

        public abstract void Send();
        public abstract Task SendAsync();
    }
}