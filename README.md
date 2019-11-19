# Extensions.Email

A lightweight and extensible library for building and sending emails from .NET applications.

## Install

Install the nuget packages `CASPR.Extensions.Email`

## Setup

Add Email to services.

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

// every time you want to send new email, call Create()
email = _emailFactory.Create();
// using a template
email
    .To("user@email.com")
    .UsingTemplate<NewsLetterModel>("newsletter", model)
    .Send()
    ;
```

## Senders

See in [extensions list](https://github.com/casprsoftware/Extensions).
