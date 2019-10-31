using System.Globalization;
using System.Threading.Tasks;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// Represents email template storage
    /// </summary>
    public interface IEmailTemplateStorage
    {
        /// <summary>
        /// Get a template by name and culture
        /// </summary>
        /// <param name="templateName">The template name.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns></returns>
        Task<IEmailTemplate> GetTemplateAsync(string templateName, CultureInfo cultureInfo = null);
    }
}
