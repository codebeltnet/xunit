namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperMinimalConsole.App;

public sealed class BootstrapperMinimalConsoleMarker
{
    public BootstrapperMinimalConsoleMarker(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static string LastValue { get; set; }
}
