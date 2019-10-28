# Email Framework

Email Framework is a lightweight and extensible library for building and sending emails from .NET applications

## Install

Install packages via `dotnet`, `Install-Package` or from VS.

Package name                              | Description                      
------------------------------------------|-----------------------------
`CASPR.EmailFramework.Core`                 | Core functionality and interfaces
*Optional* |
`CASPR.EmailFramework.Senders.SendGrid`     | SendGrid email sender implementation
`CASPR.EmailFramework.TemplateEngines.Razor` | Razor email template engine

## Setup

Add Email Framework to services.

```csharp
services.AddEmailFramework(options=>{
    // set default from email address
    options.DefaultFrom = "from@example.com";
    options.DefaultFromName = "Email Test Program";
})
.AddSendGridSender(options=>{
    options.ApiKey = "paste your SendGrid API Key here";
})
.AddRazorTemplateEngine();
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
