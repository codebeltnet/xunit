using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting.Assets
{
    public class MinimalValidHostTest : MinimalHostTest<ManagedMinimalHostFixture>
    {
        public MinimalValidHostTest(ManagedMinimalHostFixture hostFixture) : base(hostFixture)
        {
        }
    }
}
