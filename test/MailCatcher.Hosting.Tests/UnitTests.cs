using Microsoft.Extensions.DependencyInjection;
using System.Net.Sockets;

namespace MailCatcher.Hosting.Tests;

public class UnitTests
{
    [Fact(Skip = "future problem")]
    public void AddMailCatcherContainerWithDefaultsAddsAnnotationMetadata()
    {
        var builder = DistributedApplication.CreateBuilder();

        builder.AddMailCatcher("mailcatcher");

        using var app = builder.Build();

        var appModel = app.Services.GetRequiredService<DistributedApplicationModel>();

        var containerResource = Assert.Single(appModel.Resources.OfType<MailCatcherResource>());
        Assert.Equal("mailcatcher", containerResource.Name);

        var endpoint = Assert.Single(containerResource.Annotations.OfType<EndpointAnnotation>());
        Assert.Equal(8123, endpoint.TargetPort);
        Assert.False(endpoint.IsExternal);
        Assert.Equal("http", endpoint.Name);
        Assert.Null(endpoint.Port);
        Assert.Equal(ProtocolType.Tcp, endpoint.Protocol);
        Assert.Equal("tcp", endpoint.Transport);
        Assert.Equal("tcp", endpoint.UriScheme);

        var containerAnnotation = Assert.Single(containerResource.Annotations.OfType<ContainerImageAnnotation>());
        Assert.Equal(MailCatcherContainerImageTags.Tag, containerAnnotation.Tag);
        Assert.Equal(MailCatcherContainerImageTags.Image, containerAnnotation.Image);
        Assert.Equal(MailCatcherContainerImageTags.Registry, containerAnnotation.Registry);
    }

    [Fact(Skip = "future problem")]
    public async Task MailCatcherCreatesConnectionString()
    {
        var builder = DistributedApplication.CreateBuilder();
        var mailcatcher = builder.AddMailCatcher("mailcatcher")
                                 .WithEndpoint("http", e => e.AllocatedEndpoint = new AllocatedEndpoint(e, "localhost", 1234));

        var connectionStringResource = mailcatcher.Resource as IResourceWithConnectionString;
        var connectionString = await connectionStringResource.GetConnectionStringAsync();

        Assert.Equal("Host={v.bindings.http.host};Protocol=http;Port={mailcatcher.bindings.http.port};Username=default;Password={mailcatcher-password.value}", connectionStringResource.ConnectionStringExpression.ValueExpression);
    }
}