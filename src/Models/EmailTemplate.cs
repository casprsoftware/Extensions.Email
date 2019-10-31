using System.Globalization;

namespace CASPR.Extensions.Email.Models
{
    /// <summary>
    /// Email Template
    /// </summary>
    public class EmailTemplate : IEmailTemplate
    {
        public EmailTemplate(string name, CultureInfo culture = null)
        {
            Name = name;
            Culture = culture;
        }

        #region Public Properties

        /// <summary>
        /// The template name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Culture info of the template
        /// </summary>
        public CultureInfo Culture { get; }

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
