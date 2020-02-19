using CASPR.Extensions.Email.Models;
using Microsoft.Extensions.Options;

namespace CASPR.Extensions.Email
{
    public class QueueEmailFactory : IEmailFactory
    {
        private readonly IEmailTemplateEngine _emailTemplateEngine;
        private readonly IEmailTemplateStorage _emailTemplateStorage;
        private readonly EmailOptions _emailOptions;
        private readonly IEmailMessageQueue _pendingEmailMessageQueue;

        public QueueEmailFactory(
            IEmailTemplateEngine emailTemplateEngine, 
            IEmailTemplateStorage emailTemplateStorage, 
            IEmailMessageQueue pendingEmailMessageQueue,
            IOptions<EmailOptions> optionAccessor)
        {
            _emailOptions = optionAccessor.Value;
            _emailTemplateEngine = emailTemplateEngine;
            _emailTemplateStorage = emailTemplateStorage;
            _pendingEmailMessageQueue = pendingEmailMessageQueue;
        }

        public EmailBuilder Create()
        {
            var from = new EmailAddress(_emailOptions.DefaultFrom, _emailOptions.DefaultFromName);
            return new QueueEmailBuilder(
                _emailTemplateEngine,
                _emailTemplateStorage,
                from,
                _pendingEmailMessageQueue);
        }
    }
}