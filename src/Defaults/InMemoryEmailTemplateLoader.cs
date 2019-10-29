using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email.Defaults
{
    /// <summary>
    /// Get email templates from memory
    /// </summary>
    public class InMemoryEmailTemplateLoader : IEmailTemplateLoader
    {
        private readonly ConcurrentDictionary<long, EmailTemplate> _templates;

        public InMemoryEmailTemplateLoader(IDictionary<long, EmailTemplate> templates)
        {
            _templates = new ConcurrentDictionary<long, EmailTemplate>(templates);
        }

        public Task<EmailTemplate> GetTemplateAsync(long templateId, CultureInfo cultureInfo = null)
        {
            if (_templates.TryGetValue(templateId, out var tmp))
            {
                return Task.FromResult(tmp);
            }

            return Task.FromResult<EmailTemplate>(null);
        }
    }
}
