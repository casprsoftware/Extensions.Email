namespace CASPR.Extensions.Email
{
    /// <summary>
    /// Email factory
    /// </summary>
    public interface IEmailFactory
    {
        /// <summary>
        /// Create new instance of <see cref="EmailBuilder"/>.
        /// </summary>
        /// <returns>New <see cref="EmailBuilder"/> instance.</returns>
        EmailBuilder Create();
    }
}
