using System.Threading.Tasks;

namespace CASPR.Extensions.Email
{
    public interface IEmailTemplateEngine
    {
        Task<string> RenderAsync<T>(
            string templateKey,
            string templateContent,
            T model, 
            bool isHtml = true);
    }
}
