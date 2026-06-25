---
uid: Codebelt.Extensions.Xunit.Hosting.ServiceProviderExtensions
example:
- *content
---

The following example retrieves a scoped service of type `T` from an `IServiceProvider` by calling `GetRequiredScopedService<T>`. The extension creates a new scope, resolves the service from the scope's provider, and disposes the scope before returning.

```csharp
using System;
using Codebelt.Extensions.Xunit.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HostFixtureTests;

public class ServiceResolver
{
    public T Resolve<T>(IServiceProvider provider) where T : notnull
    {
        return provider.GetRequiredScopedService<T>();
    }
}
```
