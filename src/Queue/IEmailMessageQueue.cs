using System.Threading.Tasks;
using CASPR.Extensions.Email.Models;

namespace CASPR.Extensions.Email
{
    public interface IEmailMessageQueue
    {
        Task AddAsync(EmailMessage message);
        Task<EmailMessage> NextAsync();
    }
}