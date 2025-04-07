using System;
using Microsoft.AspNetCore.Builder;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    /// <summary>
    /// Provides a way to use Microsoft Dependency Injection in unit tests (minimal style).
    /// </summary>
    /// <seealso cref="IMinimalHostFixture" />
    /// <seealso cref="IPipelineTest" />
    public interface IMinimalWebHostFixture : IMinimalHostFixture, IPipelineTest
    {
        /// <summary>
        /// Gets or sets the delegate that configures the HTTP request pipeline.
        /// </summary>
        /// <value>The delegate that configures the HTTP request pipeline.</value>
        Action<IApplicationBuilder> ConfigureApplicationCallback { get; set; }
    }
}
