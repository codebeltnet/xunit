## About

An open-source project (MIT license) that targets and complements the Microsoft .NET platform. It provides vast ways of possibilities for all breeds of coders, programmers, developers and the likes thereof.
Your ideal companion for .NET 8, .NET 7, .NET 6, .NET Standard 2 and .NET Framework 4.6.2 and newer.

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

Source: [AspNetCoreHostTestTest.cs](https://github.com/codebeltnet/xunit/tree/main/test/Codebelt.Extensions.Xunit.Hosting.AspNetCore.Tests/AspNetCoreHostTestTest.cs)

```csharp
public class AspNetCoreHostTestTest : AspNetCoreHostTest<AspNetCoreHostFixture>
{
    private readonly IServiceProvider _provider;

    public AspNetCoreHostTestTest(AspNetCoreHostFixture hostFixture, ITestOutputHelper output) : base(hostFixture, output)
    {
        _provider = hostFixture.ServiceProvider;
        _provider.GetRequiredService<ITestOutputHelperAccessor>().TestOutput = output;
    }

    // intentionally left out for brevity

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddXunitTestOutputHelperAccessor();
        services.AddXunitTestLogging(new TestOutputHelperAccessor(TestOutput));
    }
}
```
