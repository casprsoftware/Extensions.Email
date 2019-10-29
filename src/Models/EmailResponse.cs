using System.Collections.Generic;
using System.Linq;

namespace CASPR.Extensions.Email.Models
{
    /// <summary>
    /// Email Sending Response
    /// </summary>
    public class EmailResponse
    {
        #region Constructor

        public EmailResponse(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public EmailResponse() : this(Enumerable.Empty<string>())
        {
        }

        #endregion

        #region Public Properties

        public IEnumerable<string> ErrorMessages { get; }
        public bool Successful => !ErrorMessages.Any();

        #endregion
    }
}
