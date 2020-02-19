using Microsoft.Extensions.DependencyInjection;

namespace CASPR.Extensions.Email
{
    internal class EmailConfigurationBuilder : IEmailConfigurationBuilder
    {
        public IServiceCollection Services { get; }

        public EmailConfigurationBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}