namespace Codebelt.Extensions.Xunit.Hosting.Program.App;

public sealed class ProgramCustomization
{
    public ProgramCustomization(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
