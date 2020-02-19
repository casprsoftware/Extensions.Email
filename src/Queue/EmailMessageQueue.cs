using System.Collections.Concurrent;
using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    class EmailMessageQueue : IEmailMessageQueue
    {
        private readonly ConcurrentQueue<EmailMessage> _emailMessages;

        public EmailMessageQueue()
        {
            _emailMessages = new ConcurrentQueue<EmailMessage>();
        }

        public Task AddAsync(EmailMessage message)
        {
            _emailMessages.Enqueue(message);
            return Task.CompletedTask;
        }

        public Task<EmailMessage> NextAsync()
        {
            _emailMessages.TryDequeue(out var message);
            return Task.FromResult(message);
        }
    }
}