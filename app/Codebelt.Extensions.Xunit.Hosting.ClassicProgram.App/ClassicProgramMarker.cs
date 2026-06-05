namespace Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App;

public sealed class ClassicProgramMarker
{
    public ClassicProgramMarker(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
