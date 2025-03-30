using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    internal sealed class XunitTestLoggerProvider : InMemoryTestStore<XunitTestLoggerEntry>, ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, XunitTestLogger> _loggers = new();
        private readonly ITestOutputHelperAccessor _accessor;
        private readonly ITestOutputHelper _output;

        public XunitTestLoggerProvider(ITestOutputHelper output)
        {
            _output = output;
        }

        public XunitTestLoggerProvider(ITestOutputHelperAccessor accessor)
        {
            _accessor = accessor;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, _ => _accessor != null
                ? new XunitTestLogger(this, _accessor)
                : new XunitTestLogger(this, _output));
        }

        public void WriteLoggerEntry(LogLevel logLevel, EventId eventId, string message)
        {
            Add(new XunitTestLoggerEntry(logLevel, eventId, message));
        }

        public void Dispose()
        {
        }
    }
}
