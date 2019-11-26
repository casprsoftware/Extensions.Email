﻿using CASPR.Extensions.Email.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CASPR.Extensions.Email
{
    public class DefaultEmailFactory : IEmailFactory
    {
        private readonly ILogger _logger;
        private readonly EmailOptions _emailOptions;
        private readonly IEmailTemplateEngine _emailTemplateEngine;
        private readonly IEmailTemplateStorage _emailTemplateStorage;
        private readonly IEmailSender _emailSender;

        public DefaultEmailFactory(
            IOptions<EmailOptions> emailOptions, 
            IEmailTemplateEngine emailTemplateEngine,
            IEmailTemplateStorage emailTemplateStorage,
            IEmailSender emailSender,
            ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DefaultEmailFactory>();
            _emailOptions = emailOptions.Value;
            _emailTemplateEngine = emailTemplateEngine;
            _emailTemplateStorage = emailTemplateStorage;
            _emailSender = emailSender;
        }

        public EmailBuilder Create()
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Create an email [DefaultFrom={From}, Sender={Sender}, TemplateRenderer={TemplateRenderer}]", 
                    _emailOptions.DefaultFrom,
                    _emailSender.GetType().Name,
                    _emailTemplateEngine.GetType().Name);
            }
            var from = new EmailAddress(_emailOptions.DefaultFrom, _emailOptions.DefaultFromName);
            
            return new DefaultEmailBuilder(
                _emailSender, 
                _emailTemplateEngine,
                _emailTemplateStorage, 
                from);
        }
    }
}
