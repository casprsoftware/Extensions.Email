using System;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    public static class EmailBuilderExtensions
    {
        /// <summary>
        /// Add email address to recipient
        /// </summary>
        /// <param name="emailBuilder"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EmailBuilder To(this EmailBuilder emailBuilder, string email, string name = null)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return emailBuilder.To(new EmailAddress(email, name));
        }

        /// <summary>
        /// Add email address to carbon copy
        /// </summary>
        /// <param name="emailBuilder"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EmailBuilder Cc(this EmailBuilder emailBuilder, string email, string name = null)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return emailBuilder.Cc(new EmailAddress(email, name));
        }

        /// <summary>
        /// Add email address to blind carbon copy
        /// </summary>
        /// <param name="emailBuilder"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static EmailBuilder Bcc(this EmailBuilder emailBuilder, string email, string name = null)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return emailBuilder.Bcc(new EmailAddress(email, name));
        }
    }
}