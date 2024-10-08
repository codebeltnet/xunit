using System;
using System.Threading.Tasks;
using Cuemon.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore.Assets
{
    public class BoolMiddleware : ConfigurableMiddleware<BoolOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoolMiddleware"/> class.
        /// </summary>
        /// <param name="next">The delegate of the request pipeline to invoke.</param>
        /// <param name="setup">The <see cref="BoolMiddleware" /> which need to be configured.</param>
        public BoolMiddleware(RequestDelegate next, IOptions<BoolOptions> setup) : base(next, setup)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolMiddleware"/> class.
        /// </summary>
        /// <param name="next">The delegate of the request pipeline to invoke.</param>
        /// <param name="setup">The <see cref="BoolMiddleware"/> which need to be configured.</param>
        public BoolMiddleware(RequestDelegate next, Action<BoolOptions> setup) : base(next, setup)
        {
        }

        /// <summary>
        /// Executes the <see cref="BoolMiddleware" />.
        /// </summary>
        /// <param name="context">The context of the current request.</param>
        /// <returns>A task that represents the execution of this middleware.</returns>
        public override async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync($"A:{Options.A}, B:{Options.B}, C:{Options.C}, D:{Options.D}, E:{Options.E}, F:{Options.F}");
            await Next(context);
        }
    }
}
