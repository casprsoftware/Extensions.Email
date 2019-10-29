using System.Globalization;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// Represents email template loader
    /// </summary>
    public interface IEmailTemplateLoader
    {
        /// <summary>
        /// Get a template by name and culture
        /// </summary>
        /// <param name="templateId">The template ID.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        Task<EmailTemplate> GetTemplateAsync(long templateId, CultureInfo cultureInfo = null);
    }
}
