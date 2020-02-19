using CASPR.Extensions.Email.TemplateEngines;
using Microsoft.Extensions.DependencyInjection;

namespace CASPR.Extensions.Email
{
    public static class EmailConfigurationBuilderExtensions
    {
        /// <summary>
        /// Add Razor template engine
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IEmailConfigurationBuilder AddRazorTemplateEngine(
            this IEmailConfigurationBuilder builder)
        {
            var descriptor = ServiceDescriptor.Transient<IEmailTemplateEngine, NullEmailTemplateEngine>();
            builder.Services.Remove(descriptor);
            builder.Services.AddSingleton<IEmailTemplateEngine, RazorTemplateEngine>();
            return builder;
        }
    }
}