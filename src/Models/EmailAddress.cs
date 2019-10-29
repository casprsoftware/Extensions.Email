namespace CASPR.Extensions.Email.Models
{
    /// <summary>
    /// Represents an Email Address
    /// </summary>
    public class EmailAddress
    {
        #region Constructor
        /// <summary>
        /// <see cref="EmailAddress"/> constructor
        /// </summary>
        /// <param name="value">The value of email address.</param>
        /// <param name="name">The name of owner of email address.</param>
        public EmailAddress(string value, string name = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new System.ArgumentException("Email address value cannot be null or empty.", nameof(value));
            }

            Name = name;
            Value = value;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Name of email address
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Value { get; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Value;
            }
            return $"{Name} <{Value}>";
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is EmailAddress))
            {
                return false;
            }
            var emailAddressObj = (EmailAddress)obj;
            return emailAddressObj.Value == Value;
        }
        #endregion
    }
}
