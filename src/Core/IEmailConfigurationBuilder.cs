using Microsoft.Extensions.DependencyInjection;

namespace CASPR.Extensions.Email
{
    /// <summary>
    /// An interface for configuring email providers.
    /// </summary>
    public interface IEmailConfigurationBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where Email services are configured.
        /// </summary>
        IServiceCollection Services { get; }
    }
}