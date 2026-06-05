using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Codebelt.Extensions.Xunit.Hosting.Program.App;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton(new ProgramMarker("Modern Program"));

        var app = builder.Build();

        app.MapGet("/", (ProgramMarker marker, IHostEnvironment environment) => $"{marker.Value}|{environment.EnvironmentName}");
        app.MapGet("/configuration", (IConfiguration configuration) => configuration["ProgramLane:Message"] ?? "Missing");
        app.MapGet("/custom-service", (IEnumerable<ProgramCustomization> customizations) =>
        {
            foreach (var customization in customizations)
            {
                return customization.Value;
            }

            return "Missing";
        });

        app.Run();
    }
}
