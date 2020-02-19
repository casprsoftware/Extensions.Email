# Extensions.Email

.NET standard library for building and sending emails.

## Installation

Use the nuget package manager to install the package(s):

- `CASPR.Extensions.Email.Core` - Core library.
- `CASPR.Extensions.Email.SendGrid` - SendGrid email sender.
- `CASPR.Extensions.Email.Razor` - Razor email templates.
- `CASPR.Extensions.Email.Queue` - Sending emails in background task.

## Setup

Add Email to Dependency Injection (`IServiceCollection`).

```csharp
services.AddEmail(builder => {

    // Set defaults
    builder.SetOptions(options =>
    {
        options.DefaultFrom = "noreply@casprsoftware.com";
        options.DefaultFromName = "Email Test Program";
    });

    // Register templates (optional)
    emailBuilder.AddInMemoryTemplateStorage(new[]
    {
        new EmailTemplate("template_1")
        {
            Subject = "template subject",
            Body = "template body.."
        }
    });

    // Add SendGrid sender (optional)
    builder.AddSendGridSender(options => {
        options.ApiKey = "Your Sendgrid API key";
    });

    // Add Razor template engine (optional)
    emailBuilder.AddRazorTemplateEngine();

    // Add queue and the end of the configuration (optional)
    emailBuilder.AddQueue();
});
```

## Usage

Inject `IEmailFactory` as a dependency where you want to send an email.

```csharp
// create new instance of EmailBuilder
var email = _emailFactory.Create();

// build an email and send it
email
    .To("user@email.com")
    .Body("some text")
    .Subject("...")
    .Send()
    ;
```

Using a template.

```csharp
// every time you want to send new email, call Create()
email = _emailFactory.Create();
// using a template
email
    .To("user@email.com")
    .UsingTemplate<NewsLetterModel>("template_1", model)
    .Send()
    ;
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)