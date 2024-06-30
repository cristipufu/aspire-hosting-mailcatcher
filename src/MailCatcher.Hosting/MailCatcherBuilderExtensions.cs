using Aspire.Hosting.ApplicationModel;
using MailCatcher.Hosting;

namespace Aspire.Hosting;

/// <summary>
/// Provides extension methods for adding MailCatcher resources to an <see cref="IDistributedApplicationBuilder"/>.
/// </summary>
public static class MailCatchervResourceBuilderExtensions
{
    /// <summary>
    /// Adds the <see cref="MailCatcherResource"/> to the given
    /// <paramref name="builder"/> instance. Uses the "0.8.2" tag.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="httpPort">The HTTP port.</param>
    /// <param name="smtpPort">The SMTP port.</param>
    /// <returns>
    /// An <see cref="IResourceBuilder{MailCatcherResource}"/> instance that
    /// represents the added MailCatcher resource.
    /// </returns>
    public static IResourceBuilder<MailCatcherResource> AddMailCatcher(
        this IDistributedApplicationBuilder builder,
        string name,
        int? httpPort = null,
        int? smtpPort = null)
    {
        var resource = new MailCatcherResource(name);

        return builder.AddResource(resource)
                      .WithImage(MailCatcherContainerImageTags.Image)
                      .WithImageRegistry(MailCatcherContainerImageTags.Registry)
                      .WithImageTag(MailCatcherContainerImageTags.Tag)
                      .WithHttpEndpoint(
                          targetPort: 1080,
                          port: httpPort,
                          name: MailCatcherResource.HttpEndpointName)
                      .WithEndpoint(
                          targetPort: 1025,
                          port: smtpPort,
                          name: MailCatcherResource.SmtpEndpointName);
    }
}