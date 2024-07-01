# MailCatcher.Hosting library

[![NuGet](https://img.shields.io/nuget/v/MailCatcher.Hosting)](https://www.nuget.org/packages/MailCatcher.Hosting)
[![GitHub](https://img.shields.io/github/license/cristipufu/aspire-hosting-mailcatcher)](https://github.com/cristipufu/aspire-hosting-mailcatcher/blob/master/LICENSE)

Provides extension methods and resource definitions for a .NET Aspire AppHost to configure a MailCatcher resource.

## Getting started

### Install the package

In your AppHost project, install the .NET Aspire MailCatcher Hosting library with NuGet:

```dotnetcli
dotnet add package MailCatcher.Hosting
```

## Usage example

Then, in the _Program.cs_ file of `AppHost`, add a MailCatcher resource and consume the connection using the following methods:

```csharp
var mail = builder.AddMailCatcher("mailcatcher");

var api = builder.AddProject<Projects.MailCatcher_DemoApi>("api")
       .WithReference(mail);
```

In the _Program.cs_ file of the `WebApi`, configure the `SmtpClient`:
```charp
builder.Services.AddSingleton<SmtpClient>(sp =>
{
    var smtpUri = new Uri(builder.Configuration.GetConnectionString("mailcatcher")!);

    var smtpClient = new SmtpClient(smtpUri.Host, smtpUri.Port);

    return smtpClient;
});

app.MapPost("/subscribe", async ([FromServices] SmtpClient smtpClient, string email) =>
{
    using var message = new MailMessage("hi@company.com", email)
    {
        Subject = "Welcome to our app!",
        Body = "Thank you for subscribing!"
    };

    await smtpClient.SendMailAsync(message);
});
```

```http
POST /subscribe?email=test@test.com HTTP/1.1
Host: localhost:7251
Content-Type: application/json
```
## Feedback & contributing

Contributions are welcome! Whether you're fixing a bug, adding a new feature, or improving the documentation, please feel free to make a pull request.
