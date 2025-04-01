using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    public class ServiceCollectionExtensions : Test
    {
        public ServiceCollectionExtensions(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void AddXunitTestLogging_ShouldAddXunitTestLogging()
        {
            var services = new ServiceCollection();
            services.AddXunitTestLogging(TestOutput);

            var provider = services.BuildServiceProvider();

            var logger1 = provider.GetRequiredService<ILogger<ServiceCollectionExtensions>>();
            var loggerStore1 = logger1.GetTestStore();
            var loggerStore1Duplicate = logger1.GetTestStore(typeof(ServiceCollectionExtensions).FullName!.ToLowerInvariant());

            var logger2 = provider.GetRequiredService<ILogger<Test>>();
            var loggerStore2 = logger2.GetTestStore(null); // all loggers

            logger1.LogCritical("SUT");
            logger1.LogTrace("SUT");
            logger1.LogDebug("SUT");
            logger1.LogError("SUT");
            logger1.LogInformation("SUT");
            logger1.LogWarning("SUT");

            logger2.LogInformation("Unique message for logger2.");

            Assert.Throws<KeyNotFoundException>(() => logger2.GetTestStore("InvalidValue"));

            Assert.Equal(loggerStore1, loggerStore1Duplicate);

            Assert.NotNull(logger1);
            Assert.NotNull(loggerStore1);
            
            Assert.NotNull(logger2);
            Assert.NotNull(loggerStore2);

            Assert.Equal(6, loggerStore1.Query().Count());
            Assert.Equal(7, loggerStore2.Query().Count());
            
            Assert.Collection(loggerStore1.Query(),
                entry => Assert.Equal("Critical: SUT", entry.ToString()),
                entry => Assert.Equal("Trace: SUT", entry.ToString()),
                entry => Assert.Equal("Debug: SUT", entry.ToString()),
                entry => Assert.Equal("Error: SUT", entry.ToString()),
                entry => Assert.Equal("Information: SUT", entry.ToString()),
                entry => Assert.Equal("Warning: SUT", entry.ToString()));

            Assert.Collection(loggerStore2.Query(),
                entry => Assert.Equal("Critical: SUT", entry.ToString()),
                entry => Assert.Equal("Trace: SUT", entry.ToString()),
                entry => Assert.Equal("Debug: SUT", entry.ToString()),
                entry => Assert.Equal("Error: SUT", entry.ToString()),
                entry => Assert.Equal("Information: SUT", entry.ToString()),
                entry => Assert.Equal("Warning: SUT", entry.ToString()),
                entry => Assert.Equal("Information: Unique message for logger2.", entry.ToString()));

        }
    }
}
