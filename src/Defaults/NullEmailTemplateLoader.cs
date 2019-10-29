using System.Globalization;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email.Defaults
{
    public class NullEmailTemplateLoader : IEmailTemplateLoader
    {
        public Task<EmailTemplate> GetTemplateAsync(long templateId, CultureInfo cultureInfo = null)
        {
            return Task.FromResult<EmailTemplate>(null);
        }
    }
}
