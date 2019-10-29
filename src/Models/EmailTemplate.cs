namespace CASPR.Extensions.Email.Models
{
    /// <summary>
    /// Email Template
    /// </summary>
    public class EmailTemplate
    {
        #region Public Properties

        /// <summary>
        /// The template ID
        /// </summary>
        public long Id { get; set; }
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
