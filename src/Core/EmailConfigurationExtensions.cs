using CASPR.Extensions.Email.Models;
using CASPR.Extensions.Email.Senders;
using CASPR.Extensions.Email.TemplateStorages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// Extension methods for setting up email services in an <see cref="IEmailConfigurationBuilder" />.
    /// </summary>
    public static class EmailConfigurationExtensions
    {
        public static IEmailConfigurationBuilder SetOptions(
            this IEmailConfigurationBuilder configurationBuilder, Action<EmailOptions> configure)
        {
            configurationBuilder
                .Services
                .Configure<EmailOptions>(configure);

            return configurationBuilder;
        }

        public static IEmailConfigurationBuilder AddFactory<TFactory>(
            this IEmailConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Services
                .Replace(ServiceDescriptor.Transient(typeof(IEmailFactory), typeof(TFactory)));

            return configurationBuilder;
        }

        public static IEmailConfigurationBuilder AddInMemoryTemplateStorage(
            this IEmailConfigurationBuilder configurationBuilder, IEnumerable<EmailTemplate> templates)
        {
            var descriptor = ServiceDescriptor.Transient<IEmailTemplateStorage, NullEmailTemplateStorage>();
            configurationBuilder.Services.Remove(descriptor);
            configurationBuilder.Services.AddSingleton<IEmailTemplateStorage>(new InMemoryEmailTemplateStorage(templates));
            return configurationBuilder;
        }

        public static IEmailConfigurationBuilder AddSendToFileSender(this IEmailConfigurationBuilder configurationBuilder, Action<SendToFileEmailSenderOptions> configure)
        {
            configurationBuilder.Services.Configure<SendToFileEmailSenderOptions>(configure);
            configurationBuilder.Services.Replace(ServiceDescriptor.Transient(typeof(IEmailSender), typeof(SendToFileEmailSender)));
            return configurationBuilder;
        }
    }
}