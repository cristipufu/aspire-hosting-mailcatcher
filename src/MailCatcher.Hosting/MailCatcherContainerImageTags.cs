using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MailCatcher.Hosting.Tests")]
namespace MailCatcher.Hosting;

internal static class MailCatcherContainerImageTags
{
    public const string Registry = "docker.io";
    public const string Image = "dockage/mailcatcher";
    public const string Tag = "0.8";
}
