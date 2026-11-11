## About

An open-source project (MIT license) that targets and complements the [xUnit.net](https://xunit.net/) test platform. It provides a uniform and convenient way of doing unit test for all project types in .NET.

This package targets `.NET 11`, `.NET 10` and `.NET Standard 2.0`. Its .NET Standard asset may be used by older .NET and .NET Framework projects when their APIs, dependencies and test runner are compatible.

It is, by heart, free, flexible and built to extend and boost your agile codebelt.

## **Codebelt.Extensions.Xunit** for .NET

The `Codebelt.Extensions.Xunit` namespace contains types that provides a uniform way of doing unit testing. The namespace relates to the `Xunit.Abstractions` namespace.

More documentation available at our documentation site:

- [Codebelt.Extensions.Xunit](https://xunit.codebelt.net/api/Codebelt.Extensions.Xunit.html) 🔗

## Related Packages

* [Codebelt.Extensions.Xunit](https://www.nuget.org/packages/Codebelt.Extensions.Xunit/) 📦
* [Codebelt.Extensions.Xunit.App](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.App/) 🏭
* [Codebelt.Extensions.Xunit.Hosting](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.Hosting/) 📦
* [Codebelt.Extensions.Xunit.Hosting.AspNetCore](https://www.nuget.org/packages/Codebelt.Extensions.Xunit.Hosting.AspNetCore/) 📦

### CSharp Example

Source: [TestTest.cs](https://github.com/codebeltnet/xunit/tree/main/test/Codebelt.Extensions.Xunit.Tests/TestTest.cs)

```csharp
public class TestTest : Test
{
    public TestTest(ITestOutputHelper output) : base(output)
    {
    }

    // intentionally left out for brevity

    protected override void OnDisposeManagedResources()
    {
        base.OnDisposeManagedResources();
    }
}
```
