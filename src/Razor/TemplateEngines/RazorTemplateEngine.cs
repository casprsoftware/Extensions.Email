using System.Threading.Tasks;
using RazorLight;

namespace CASPR.Extensions.Email.TemplateEngines
{
    public class RazorTemplateEngine : IEmailTemplateEngine
    {
        private readonly RazorLightEngine _engine;

        public RazorTemplateEngine()
        {
            _engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderAsync<T>(
            string templateKey, 
            string templateContent, T model, bool isHtml = true)
        {
            var cacheResult = _engine.TemplateCache.RetrieveTemplate(templateKey);
            if (cacheResult.Success)
            {
                string result = await _engine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), model);
                return result;
            }
            return await _engine
                .CompileRenderAsync(templateKey, templateContent, model);
        }
    }
}
