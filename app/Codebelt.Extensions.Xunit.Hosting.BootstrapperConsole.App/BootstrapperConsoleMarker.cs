namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperConsole.App;

public sealed class BootstrapperConsoleMarker
{
    public BootstrapperConsoleMarker(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static string LastValue { get; set; }
}
