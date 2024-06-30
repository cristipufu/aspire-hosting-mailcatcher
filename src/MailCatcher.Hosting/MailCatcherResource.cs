namespace Aspire.Hosting.ApplicationModel;

public sealed class MailCatcherResource(string name) : ContainerResource(name), IResourceWithConnectionString
{
    // Constants used to refer to well known-endpoint names, this is specific
    // for each resource type. MailCatcher exposes an SMTP endpoint and a HTTP
    // endpoint.
    internal const string SmtpEndpointName = "smtp";
    internal const string HttpEndpointName = "http";

    // An EndpointReference is a core .NET Aspire type used for keeping
    // track of endpoint details in expressions. Simple literal values cannot
    // be used because endpoints are not known until containers are launched.
    private EndpointReference? _smtpReference;

    public EndpointReference SmtpEndpoint =>
        _smtpReference ??= new(this, SmtpEndpointName);

    // Required property on IResourceWithConnectionString. Represents a connection
    // string that applications can use to access the MailCatcher server. In this case
    // the connection string is composed of the SmtpEndpoint endpoint reference.
    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create(
            $"smtp://{SmtpEndpoint.Property(EndpointProperty.Host)}:{SmtpEndpoint.Property(EndpointProperty.Port)}"
        );
}