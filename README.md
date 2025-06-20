# Common.Messaging

Common.Messaging is a reusable .NET class library for managing email sending, messaging contracts, and integration with SMTP or other delivery services. It is designed for use in microservice-based architectures.

## Features

- `EmailRequest` model for strongly-typed message payloads
- `IEmailSender` interface for flexible transport implementations
- `SmtpEmailSender` implementation using `System.Net.Mail`
- Designed for DI with `Microsoft.Extensions.Logging` and `IOptions<T>`
- Supports attachments as Base64 strings

## Requirements

- .NET 6, 7, or 8
- `Microsoft.Extensions.Logging`
- `Microsoft.Extensions.Options`

## Usage

```csharp
var email = new EmailRequest
{
    To = "user@example.com",
    Subject = "Welcome!",
    BodyHtml = "<p>Hello and welcome!</p>"
};

await _emailSender.SendAsync(email);
```

## 📄 License

MIT – feel free to use and extend.
