using System;
using CASPR.Extensions.Email.Senders;
using CASPR.Extensions.Email.TemplateEngines;
using CASPR.Extensions.Email.TemplateStorages;
using Microsoft.Extensions.DependencyInjection;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// Extension methods for setting up email services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Adds email services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="IEmailConfigurationBuilder"/> configuration delegate.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddEmail(
            this IServiceCollection services, Action<IEmailConfigurationBuilder> configure)
        {
            // add default implementations
            services.AddTransient<IEmailSender, NullEmailSender>();
            services.AddTransient<IEmailTemplateEngine, NullEmailTemplateEngine>();
            services.AddTransient<IEmailTemplateStorage, NullEmailTemplateStorage>();
            services.AddTransient<IEmailFactory, DefaultEmailFactory>();

            configure(new EmailConfigurationBuilder(services));
            return services;
        }
    }
}