# Extensions.Email

.NET standard library for building and sending emails.

## Installation

Use the nuget package manager to install the package `CASPR.Extensions.Email`.

## Setup

Add Email to Dependency Injection (`IServiceCollection`).

```csharp
services.AddEmail(builder => {
    
    builder.SetOptions(options =>
    {
        options.DefaultFrom = "noreply@casprsoftware.com";
        options.DefaultFromName = "Email Test Program";
    });

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
    .UsingTemplate<NewsLetterModel>("newsletter", model)
    .Send()
    ;
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)