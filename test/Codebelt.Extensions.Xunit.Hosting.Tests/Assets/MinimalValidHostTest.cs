using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.Assets
{
    public class MinimalValidHostTest : MinimalHostTest<MinimalHostFixture>
    {
        public MinimalValidHostTest(MinimalHostFixture hostFixture) : base(hostFixture)
        {
        }
    }
}
