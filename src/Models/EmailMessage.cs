using System.Collections.Generic;

namespace CASPR.Extensions.Email.Models
{
    /// <summary>
    /// Represents Email Message
    /// </summary>
    public class EmailMessage
    {
        #region Constructor
        public EmailMessage()
        {
            ToAddresses = new HashSet<EmailAddress>();
            CcAddresses = new HashSet<EmailAddress>();
            BccAddresses = new HashSet<EmailAddress>();
            Attachments = new HashSet<EmailAttachment>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Collection of recipients
        /// </summary>
        public HashSet<EmailAddress> ToAddresses { get; set; }
        /// <summary>
        /// Collection of carbon copy recipients
        /// </summary>
        public HashSet<EmailAddress> CcAddresses { get; set; }
        /// <summary>
        /// Collection of blind carbon copy recipients
        /// </summary>
        public HashSet<EmailAddress> BccAddresses { get; set; }
        /// <summary>
        /// ReplyTo email address
        /// </summary>
        public EmailAddress ReplyToAddress { get; set; }
        /// <summary>
        /// Email address from
        /// </summary>
        public EmailAddress FromAddress { get; set; }
        /// <summary>
        /// Subject content
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Body content.
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// If true body content is HTML, if false body content is plain text.
        /// </summary>
        public bool IsHtml { get; set; }

        /// <summary>
        /// Email priority. Default normal
        /// </summary>
        public EmailPriority Priority { get; set; } = EmailPriority.Normal;

        /// <summary>
        /// Email attachments
        /// </summary>
        public HashSet<EmailAttachment> Attachments { get; set; }
        #endregion
    }
}
