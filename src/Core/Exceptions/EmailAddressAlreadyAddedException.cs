using System;

namespace CASPR.Extensions.Email.Exceptions
{
    public class EmailAddressAlreadyAddedException : Exception
    {
        public EmailAddressAlreadyAddedException(string emailAddress)
            : base($"The email address '{emailAddress}' has been already added.")
        {
            EmailAddress = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
        }

        public string EmailAddress { get; }
    }
}
