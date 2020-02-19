using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CASPR.Extensions.Email
{
    public static class EmailConfigurationBuilderExtensions
    {
        /// <summary>
        /// Add SendGrid email sender
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IEmailConfigurationBuilder AddSendGridSender(
            this IEmailConfigurationBuilder builder,
            Action<SendGridOptions> configure)
        {
            builder.Services.Configure<SendGridOptions>(configure);
            builder.Services.Replace(ServiceDescriptor.Transient<IEmailSender, SendGridEmailSender>());
            return builder;
        }
    }
}