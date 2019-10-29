using System;
using System.IO;

namespace CASPR.Extensions.Email.Models
{
    /// <summary>
    /// The Email Attachment
    /// </summary>
    public class EmailAttachment
    {
        #region Constructor

        public EmailAttachment(string fileName, Stream data, string contentType)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            FileName = fileName;
            Data = data ?? throw new ArgumentNullException(nameof(data));
            ContentType = contentType;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// File name of the attachment
        /// </summary>
        public string FileName { get; }
        /// <summary>
        /// Content of the attachment
        /// </summary>
        public Stream Data { get; }
        /// <summary>
        /// Content type (e.g. application/pdf)
        /// </summary>
        public string ContentType { get; }

        #endregion

        #region Overrides

        public override int GetHashCode()
        {
            return FileName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is EmailAttachment))
            {
                return false;
            }
            var attachment = (EmailAttachment)obj;
            return attachment.FileName == FileName;
        }

        #endregion
    }
}