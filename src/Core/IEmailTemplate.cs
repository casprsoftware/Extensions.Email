namespace CASPR.Extensions.Email
{
    public interface IEmailTemplate
    {
        string Subject { get; }
        string Body { get; }
    }
}