using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Internal;

internal static class DeferredHostFactory
{
    public static IHost Create(Assembly assembly, Action<IHostBuilder> configureHost, bool stopApplication, bool entrypointOwned)
    {
        var deferredHostBuilder = new DeferredHostBuilder(entrypointOwned);

        deferredHostBuilder.UseEnvironment(Environments.Development);
        deferredHostBuilder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                [HostDefaults.ApplicationKey] = assembly.GetName().Name
            });
        });

        var hostFactory = ProgramHostFactoryResolver.ResolveHostFactory(assembly, stopApplication, deferredHostBuilder.ConfigureHostBuilder, deferredHostBuilder.EntryPointCompleted);
        if (hostFactory == null)
        {
            throw new InvalidOperationException($"The entry point assembly '{assembly.GetName().Name}' does not expose a supported application host.");
        }

        deferredHostBuilder.SetHostFactory(hostFactory);
        return HostBuilderFactory.Build(deferredHostBuilder, configureHost);
    }
}
