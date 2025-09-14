using System;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting
{
    public class MinimalHostTestFactoryTest : Test
    {
        public MinimalHostTestFactoryTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Create_CallerTypeShouldHaveDeclaringTypeOfMiddlewareTestFactoryTest()
        {
            Type sut1 = GetType();
            string sut2 = null;
            var middleware = MinimalHostTestFactory.Create(Assert.NotNull, host =>
              {
                  sut2 = host.Environment.ApplicationName;
              });

            Assert.True(sut1 == middleware.CallerType.DeclaringType);
            Assert.Equal(GetType().Assembly.GetName().Name, sut2);
        }


        [Fact]
        public void CreateWithHostBuilderContext_ShouldHaveApplicationNameEqualToThisAssembly_WithHostBuilderContext()
        {
            MinimalHostTestFactory.CreateWithHostBuilderContext((context, services) =>
                {
                    Assert.NotNull(context);
                    Assert.NotNull(context.HostingEnvironment);
                    Assert.NotNull(context.Configuration);
                    Assert.NotNull(context.Properties);
                    Assert.NotNull(services);
                },
                host =>
                {
                    TestOutput.WriteLine(host.Environment.ApplicationName);
                    Assert.Equal(GetType().Assembly.GetName().Name, host.Environment.ApplicationName);
                });
        }
    }
}
