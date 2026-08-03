namespace Codebelt.Extensions.Xunit.Hosting.ClassicProgram.App;

public sealed class ClassicProgramState
{
    public bool MainInvoked { get; internal set; }

    public bool EntrypointStarted { get; internal set; }
}
