using System.Globalization;

namespace CASPR.Extensions.Email.Models
{
    /// <summary>
    /// Email Template
    /// </summary>
    public class EmailTemplate : IEmailTemplate
    {
        #region Public Properties

        /// <summary>
        /// The template name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Culture info of the template
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// The email subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The email body
        /// </summary>
        public string Body { get; set; }

        #endregion
    }
}
