using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CASPR.Extensions.Email
{
    public static class EmailConfigurationBuilderExtensions
    {
        public static void AddQueue(this IEmailConfigurationBuilder builder)
        {
            builder
                .Services
                .Replace(ServiceDescriptor.Transient(typeof(IEmailFactory), typeof(QueueEmailFactory)));
            
            builder.Services.AddHostedService<QueueEmailService>();
            builder.Services.AddSingleton<IEmailMessageQueue, EmailMessageQueue>();
        }
    }
}