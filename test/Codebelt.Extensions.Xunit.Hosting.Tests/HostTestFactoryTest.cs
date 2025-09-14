using System;
using Xunit;

namespace Codebelt.Extensions.Xunit.Hosting
{
    public class HostTestFactoryTest : Test
    {
        public HostTestFactoryTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Create_CallerTypeShouldHaveDeclaringTypeOfMiddlewareTestFactoryTest()
        {
            Type sut1 = GetType();
            string sut2 = null;
            var middleware = HostTestFactory.Create(Assert.NotNull, host =>
              {
                  host.ConfigureAppConfiguration((context, _) =>
                  {
                      sut2 = context.HostingEnvironment.ApplicationName;
                  });
              });

            Assert.True(sut1 == middleware.CallerType.DeclaringType);
            Assert.Equal(GetType().Assembly.GetName().Name, sut2);
        }


        [Fact]
        public void CreateWithHostBuilderContext_ShouldHaveApplicationNameEqualToThisAssembly_WithHostBuilderContext()
        {
            HostTestFactory.CreateWithHostBuilderContext((context, services) =>
                {
                    Assert.NotNull(context);
                    Assert.NotNull(context.HostingEnvironment);
                    Assert.NotNull(context.Configuration);
                    Assert.NotNull(context.Properties);
                    Assert.NotNull(services);
                },
                host =>
                {
                    host.ConfigureAppConfiguration((context, configuration) =>
                    {
                        TestOutput.WriteLine(context.HostingEnvironment.ApplicationName);
                        Assert.Equal(GetType().Assembly.GetName().Name, context.HostingEnvironment.ApplicationName);
                    });
                });
        }
    }
}
