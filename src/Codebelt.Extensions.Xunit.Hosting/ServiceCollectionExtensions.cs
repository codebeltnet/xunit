using System;
using System.Linq;
using Cuemon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> interface.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary> 
        /// Adds a unit test optimized implementation of output logging to the <paramref name="services"/> collection. 
        /// </summary> 
        /// <param name="services">The <see cref="IServiceCollection"/> to extend.</param> 
        /// <param name="output">The <see cref="ITestOutputHelper"/> that provides the output for the logging.</param> 
        /// <param name="minimumLevel">The <see cref="LogLevel"/> that specifies the minimum level to include for the logging.</param> 
        /// <returns>A reference to <paramref name="services" /> so that additional configuration calls can be chained.</returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="services"/> cannot be null -or- 
        /// <paramref name="output"/> cannot be null. 
        /// </exception>
        /// <remarks>This method will add a xUnit logger provider based on either <see cref="ITestOutputHelper"/> or <see cref="ITestOutputHelperAccessor"/>.</remarks>
        public static IServiceCollection AddXunitTestLogging(this IServiceCollection services, ITestOutputHelper output, LogLevel minimumLevel = LogLevel.Trace) 
        { 
            Validator.ThrowIfNull(services); 
            Validator.ThrowIfNull(output);
            if (services.Any(d => d.ServiceType == typeof(ITestOutputHelperAccessor)))
            {
                services.AddLogging(builder => 
                { 
                    builder.SetMinimumLevel(minimumLevel); 
                    builder.Services.AddSingleton<ILoggerProvider>(provider =>
                    {
                        var accessor = provider.GetRequiredService<ITestOutputHelperAccessor>();
                        accessor.TestOutput = output;
                        return new XunitTestLoggerProvider(accessor);
                    });
                });
            }
            else
            {
                services.AddLogging(builder => 
                { 
                    builder.SetMinimumLevel(minimumLevel); 
                    builder.AddProvider(new XunitTestLoggerProvider(output)); 
                });
            }
            return services; 
        } 

        /// <summary>
        /// Adds a default implementation of <see cref="ITestOutputHelperAccessor"/> to the <paramref name="services"/> collection.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to extend.</param>
        /// <returns>A reference to <paramref name="services" /> so that additional configuration calls can be chained.</returns>
        public static IServiceCollection AddXunitTestLoggingOutputHelperAccessor(this IServiceCollection services)
        {
            services.AddXunitTestLoggingOutputHelperAccessor<TestOutputHelperAccessor>();
            return services;
        }

        /// <summary>
        /// Adds a specified implementation of <see cref="ITestOutputHelperAccessor"/> to the <paramref name="services"/> collection.
        /// </summary>
        /// <typeparam name="T">The type of the implementation of <see cref="ITestOutputHelperAccessor"/>.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to extend.</param>
        /// <returns>A reference to <paramref name="services" /> so that additional configuration calls can be chained.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="services"/> cannot be null.
        /// </exception>
        public static IServiceCollection AddXunitTestLoggingOutputHelperAccessor<T>(this IServiceCollection services) where T : class, ITestOutputHelperAccessor
        {
            Validator.ThrowIfNull(services);
            services.AddSingleton<ITestOutputHelperAccessor, T>();
            return services;
        }
    }
}
