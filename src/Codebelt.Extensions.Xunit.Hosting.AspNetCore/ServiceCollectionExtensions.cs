using System;
using System.ComponentModel;
using Codebelt.Extensions.Xunit.Hosting.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> interface.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a unit test optimized implementation for the <see cref="IHttpContextAccessor"/> service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to extend.</param>
        /// <param name="lifetime">The lifetime of the service. Default is <see cref="ServiceLifetime.Singleton"/>.</param>
        /// <returns>A reference to <paramref name="services"/> after the operation has completed.</returns>
        public static IServiceCollection AddFakeHttpContextAccessor(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Transient:
                    services.TryAddTransient(FakeHttpContextAccessorFactory);
                    break;
                case ServiceLifetime.Scoped:
                    services.TryAddScoped(FakeHttpContextAccessorFactory);
                    break;
                case ServiceLifetime.Singleton:
                    services.TryAddSingleton(FakeHttpContextAccessorFactory);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(lifetime), (int)lifetime, typeof(ServiceLifetime));
            }
            return services;
        }

        private static IHttpContextAccessor FakeHttpContextAccessorFactory(IServiceProvider provider)
        {
            return new FakeHttpContextAccessor(provider.GetRequiredService<IServiceScopeFactory>());
        }
    }
}
