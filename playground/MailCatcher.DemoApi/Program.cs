using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(sp =>
{
    var smtpUri = new Uri(builder.Configuration.GetConnectionString("mailcatcher")!);

    var smtpClient = new SmtpClient(smtpUri.Host, smtpUri.Port);

    return smtpClient;
});

builder.AddServiceDefaults();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/subscribe", async ([FromServices] SmtpClient smtpClient, string email) =>
{
    using var message = new MailMessage("newsletter@yourcompany.com", email)
    {
        Subject = "Welcome to our newsletter!",
        Body = "Thank you for subscribing to our newsletter!"
    };

    await smtpClient.SendMailAsync(message);
});

app.MapPost("/unsubscribe", async ([FromServices] SmtpClient smtpClient, string email) =>
{
    using var message = new MailMessage("newsletter@yourcompany.com", email)
    {
        Subject = "You are unsubscribed from our newsletter!",
        Body = "Sorry to see you go. We hope you will come back soon!"
    };

    await smtpClient.SendMailAsync(message);
});

app.Run();
