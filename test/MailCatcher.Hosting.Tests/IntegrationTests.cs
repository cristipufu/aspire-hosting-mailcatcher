using System.Net;

namespace MailCatcher.Hosting.Tests;

public class IntegrationTests
{
    [Fact(Skip = "future problem")]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.MailCatcher_AppHost>();
        await using var app = await appHost.BuildAsync();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("api");
        var response = await httpClient.GetAsync("/version/");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}