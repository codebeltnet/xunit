namespace Codebelt.Extensions.Xunit.Hosting.BootstrapperWorker.App;

public sealed class BootstrapperWorkerMarker
{
    public BootstrapperWorkerMarker(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
