using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    public class QueueEmailBuilder : EmailBuilder
    {
        private readonly IEmailMessageQueue _queue;
        public QueueEmailBuilder(
            IEmailTemplateEngine templateEngine, 
            IEmailTemplateStorage emailTemplateStorage, 
            EmailAddress defaultFrom, 
            IEmailMessageQueue queue) 
            : base(templateEngine, emailTemplateStorage, defaultFrom)
        {
            _queue = queue;
        }

        public override void Send()
        {
            _queue.AddAsync(Message)
                .GetAwaiter()
                .GetResult();
        }

        public override Task SendAsync()
        {
            return _queue.AddAsync(Message);
        }
    }
}