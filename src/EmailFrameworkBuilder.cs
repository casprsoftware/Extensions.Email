using System;
using System.Collections.Generic;
using CASPR.Extensions.Email.Defaults;
using CASPR.Extensions.Email.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CASPR.Extensions.Email
{
    public class EmailFrameworkBuilder
    {
        public IServiceCollection Services { get; }

        public EmailFrameworkBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public EmailFrameworkBuilder AddFactory<TFactory>()
        {
            Services.Replace(ServiceDescriptor.Transient(typeof(IEmailFactory), typeof(TFactory)));
            return this;
        }

        public EmailFrameworkBuilder AddInMemoryTemplateLoader(IDictionary<long, EmailTemplate> templates)
        {
            var descriptor = ServiceDescriptor.Transient<IEmailTemplateLoader, NullEmailTemplateLoader>();
            Services.Remove(descriptor);
            Services.AddSingleton<IEmailTemplateLoader>(new InMemoryEmailTemplateLoader(templates));
            return this;
        }

        public EmailFrameworkBuilder AddSendToFileSender(Action<SaveToDiskEmailSenderOptions> configure)
        {
            Services.Configure<SaveToDiskEmailSenderOptions>(configure);
            Services.Replace(ServiceDescriptor.Transient(typeof(IEmailSender), typeof(SaveToDiskEmailSender)));
            return this;
        }
    }
}