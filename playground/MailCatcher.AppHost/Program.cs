var builder = DistributedApplication.CreateBuilder(args);

var mailcatcher = builder.AddMailCatcher("mailcatcher");

builder.AddProject<Projects.MailCatcher_DemoApi>("api")
       .WithReference(mailcatcher);

builder.Build().Run();
