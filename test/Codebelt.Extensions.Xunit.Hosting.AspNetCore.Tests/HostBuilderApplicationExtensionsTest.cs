using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore;

public class HostBuilderApplicationExtensionsTest : Test
{
    public HostBuilderApplicationExtensionsTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void ToHostBuilder_ShouldReturnHostBuilder_WhenWebApplicationBuilderIsProvided()
    {
        IHostApplicationBuilder builder = WebApplication.CreateBuilder();

        var hostBuilder = builder.ToHostBuilder();

        Assert.NotNull(hostBuilder);
    }

    [Fact]
    public void ToHostBuilder_ShouldThrowArgumentException_WhenNotWebApplicationBuilder()
    {
        IHostApplicationBuilder builder = Host.CreateApplicationBuilder();

        Assert.Throws<ArgumentException>(() => builder.ToHostBuilder());
    }
}
