using CASPR.Extensions.Email.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CASPR.Extensions.Email.TemplateStorages
{
    /// <summary>
    /// Get email templates from memory
    /// </summary>
    public class InMemoryEmailTemplateStorage : IEmailTemplateStorage
    {
        private readonly IEnumerable<EmailTemplate> _templates;
        private readonly object _lock = new object();

        public InMemoryEmailTemplateStorage(IEnumerable<EmailTemplate> templates)
        {
            _templates = templates;
        }

        public Task<IEmailTemplate> GetTemplateAsync(string templateName, CultureInfo cultureInfo = null)
        {
            lock (_lock)
            {
                if (cultureInfo!=null)
                {
                    var templateWithCulture = _templates
                        .SingleOrDefault(t => t.Name == templateName && Equals(t.Culture, cultureInfo));
                    if (templateWithCulture != null)
                    {
                        return Task.FromResult<IEmailTemplate>(templateWithCulture);
                    }
                }

                var template = _templates
                    .SingleOrDefault(t => t.Name == templateName && t.Culture == null);
                return Task.FromResult<IEmailTemplate>(template);
            }
        }
    }
}
