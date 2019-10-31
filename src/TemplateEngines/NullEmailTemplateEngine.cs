using System.Threading.Tasks;

namespace CASPR.Extensions.Email.TemplateEngines
{
    public class NullEmailTemplateEngine : IEmailTemplateEngine
    {
        public Task<string> RenderAsync<T>(string templateKey, string templateContent, T model, bool isHtml = true)
        {
            return Task.FromResult(templateContent);
        }
    }
}
