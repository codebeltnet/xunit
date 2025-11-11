## About

An open-source project (MIT license) that targets and complements the [xUnit.net](https://xunit.net/) test platform. It provides a uniform and convenient way of doing unit test for all project types in .NET.

Your versatile xUnit companion for:
- Modern development with `.NET 9` and  `.NET 10`,
- Cross-platform libraries with `.NET Standard 2` (where applicable),
- Legacy applications on `.NET Framework 4.6.2` and newer.

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
