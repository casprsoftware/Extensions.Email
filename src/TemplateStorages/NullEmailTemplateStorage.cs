using System.Globalization;
using System.Threading.Tasks;

namespace CASPR.Extensions.Email.TemplateStorages
{
    public class NullEmailTemplateStorage : IEmailTemplateStorage
    {
        public Task<IEmailTemplate> GetTemplateAsync(string templateName, CultureInfo cultureInfo = null)
        {
            return Task.FromResult<IEmailTemplate>(null);
        }
    }
}
