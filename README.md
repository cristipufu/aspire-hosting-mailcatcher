# MailCatcher.Hosting library

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


## Feedback & contributing

Contributions are welcome! Whether you're fixing a bug, adding a new feature, or improving the documentation, please feel free to make a pull request.
