# Email Framework

Email Framework is a lightweight and extensible library for building and sending emails from .NET applications

## Install

`dotnet add package CASPR.Extensions.Email`

## Setup

Add Email Framework to services.

```csharp
services.AddEmail(options=>{
    // set default from email address
    options.DefaultFrom = "from@example.com";
    options.DefaultFromName = "Email Test Program";
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
