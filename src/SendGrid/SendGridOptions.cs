namespace CASPR.Extensions.Email
{
    /// <summary>
    /// SendGrid options
    /// </summary>
    public class SendGridOptions
    {
        /// <summary>
        /// API Key for access to SendGrid
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Enable SandBox mode
        /// </summary>
        public bool SandBoxMode { get; set; }
    }
}
