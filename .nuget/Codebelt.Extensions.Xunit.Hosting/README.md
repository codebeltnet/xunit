## About

An open-source project (MIT license) that targets and complements the [xUnit.net](https://xunit.net/) test platform. It provides a uniform and convenient way of doing unit test for all project types in .NET.

Your versatile xUnit companion for:
- Modern development with `.NET 9` and `.NET 10`,
- Cross-platform libraries with `.NET Standard 2` (where applicable),
- Legacy applications on `.NET Framework 4.6.2` and newer.

It is, by heart, free, flexible and built to extend and boost your agile codebelt.

## **Codebelt.Extensions.Xunit.Hosting** for .NET

The `Codebelt.Extensions.Xunit.Hosting` namespace contains types that provides a uniform way of doing unit testing that is used in conjunction with Microsoft Dependency Injection. The namespace relates to the `Xunit.Abstractions` namespace.

More documentation available at our documentation site:

- [Codebelt.Extensions.Xunit.Hosting](https://xunit.codebelt.net/api/Codebelt.Extensions.Xunit.Hosting.html) 🔗

## Related Packages

* [Codebelt.Extensions.Xunit](https://www.nuget.org/packages/Codebelt.Extensions.Xunit/) 📦
* [Codebelt.Extensions.Xunit.App](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.App/) 🏭
* [Codebelt.Extensions.Xunit.Hosting](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.Hosting/) 📦
* [Codebelt.Extensions.Xunit.Hosting.AspNetCore](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.Hosting.AspNetCore/) 📦

### CSharp Example

```csharp
public class HostTestTest : HostTest<ManagedHostFixture>
{
    private readonly IServiceProvider _provider;

    public HostTestTest(HostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
        _provider = hostFixture.Host?.Services;
        _provider.GetRequiredService<ITestOutputHelperAccessor>().TestOutput = output;
    }

    // intentionally left out for brevity

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddXunitTestLoggingOutputHelperAccessor();
        services.AddXunitTestLogging(TestOutput);
    }
}
```

A similar but real life example can be found here: [WebHostTestTest.cs](https://github.com/codebeltnet/xunit/blob/main/test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.Tests/WebHostTestTest.cs)
