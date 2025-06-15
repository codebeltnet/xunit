using System;
using System.Linq;
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
        /// Adds a unit test optimized implementation of logging to the <paramref name="services"/> collection.
        /// </summary> 
        /// <param name="services">The <see cref="IServiceCollection"/> to extend.</param> 
        /// <param name="minimumLevel">The <see cref="LogLevel"/> that specifies the minimum level to include for the logging.</param> 
        /// <returns>A reference to <paramref name="services" /> so that additional configuration calls can be chained.</returns> 
        /// <exception cref="ArgumentNullException"> 
        /// <paramref name="services"/> cannot be null.
        /// </exception> 
        public static IServiceCollection AddXunitTestLogging(this IServiceCollection services, LogLevel minimumLevel = LogLevel.Trace)
        {
            if (services == null) { throw new ArgumentNullException(nameof(services)); }
            if (services.Any(sd => sd.ServiceType == typeof(ITestOutputHelperAccessor)))
            {
                AddTestOutputHelperAccessor(services, minimumLevel);
            }
            else
            {
                services.AddLogging(builder =>
                {
                    builder.SetMinimumLevel(minimumLevel);
                    builder.AddProvider(new XunitTestLoggerProvider());
                });
            }
            return services;
        }

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
        public static IServiceCollection AddXunitTestLogging(this IServiceCollection services, ITestOutputHelper output, LogLevel minimumLevel = LogLevel.Trace)
        {
            if (services == null) { throw new ArgumentNullException(nameof(services)); }
            if (output == null) { throw new ArgumentNullException(nameof(output)); }
            if (services.Any(sd => sd.ServiceType == typeof(ITestOutputHelperAccessor)))
            {
                AddTestOutputHelperAccessor(services, minimumLevel);
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

        private static void AddTestOutputHelperAccessor(IServiceCollection services, LogLevel minimumLevel)
        {
            if (services.Any(sd => sd.ServiceType == typeof(ITestOutputHelperAccessor)))
            {
                services.AddLogging(builder =>
                {
                    builder.SetMinimumLevel(minimumLevel);
                    builder.Services.AddSingleton<ILoggerProvider>(provider =>
                    {
                        var accessor = provider.GetRequiredService<ITestOutputHelperAccessor>();
                        return new XunitTestLoggerProvider(accessor);
                    });
                });
            }
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
            if (services == null) { throw new ArgumentNullException(nameof(services)); }
            services.AddSingleton<ITestOutputHelperAccessor, T>();
            return services;
        }
    }
}
