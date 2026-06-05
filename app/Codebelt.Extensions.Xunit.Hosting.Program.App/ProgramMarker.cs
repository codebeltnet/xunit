namespace Codebelt.Extensions.Xunit.Hosting.Program.App;

public sealed class ProgramMarker
{
    public ProgramMarker(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
