using System;
using System.Collections.Generic;
using CASPR.Extensions.Email.Models;
using CASPR.Extensions.Email.Senders;
using CASPR.Extensions.Email.TemplateStorages;
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

        public EmailFrameworkBuilder AddInMemoryTemplateStorage(IEnumerable<EmailTemplate> templates)
        {
            var descriptor = ServiceDescriptor.Transient<IEmailTemplateStorage, NullEmailTemplateStorage>();
            Services.Remove(descriptor);
            Services.AddSingleton<IEmailTemplateStorage>(new InMemoryEmailTemplateStorage(templates));
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