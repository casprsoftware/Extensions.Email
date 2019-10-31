using System;
using CASPR.Extensions.Email.Senders;
using CASPR.Extensions.Email.TemplateEngines;
using CASPR.Extensions.Email.TemplateStorages;
using Microsoft.Extensions.DependencyInjection;

namespace CASPR.Extensions.Email
{
    public static class DependencyInjectionExtensions
    {
        public static EmailFrameworkBuilder AddEmailFramework(this IServiceCollection services, Action<EmailOptions> configure)
        {
            // configure
            services.Configure<EmailOptions>(configure);

            // add default implementations
            services.AddTransient<IEmailSender, NullEmailSender>();
            services.AddTransient<IEmailTemplateEngine, NullEmailTemplateEngine>();
            services.AddTransient<IEmailTemplateStorage, NullEmailTemplateStorage>();
            services.AddTransient<IEmailFactory, DefaultEmailFactory>();

            var builder = new EmailFrameworkBuilder(services);
            return builder;
        }
    }
}