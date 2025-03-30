using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    internal sealed class XunitTestLogger : ILogger, IDisposable
    {
        private readonly ITestOutputHelperAccessor _accessor;
        private readonly ITestOutputHelper _output;
        private readonly XunitTestLoggerProvider _provider;

        public XunitTestLogger(XunitTestLoggerProvider provider, ITestOutputHelper output)
        {
            _provider = provider;
            _output = output;
        }

        public XunitTestLogger(XunitTestLoggerProvider provider, ITestOutputHelperAccessor accessor)
        {
            _provider = provider;
            _accessor = accessor;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var builder = new StringBuilder($"{logLevel}: {formatter(state, exception)}");
            if (exception != null) { builder.AppendLine().Append(exception).AppendLine(); }

            var message = builder.ToString();

            _provider.WriteLoggerEntry(logLevel, eventId, message);

            if (_accessor != null)
            {
                if (_accessor.TestOutput == null) { throw new InvalidOperationException($"{nameof(ITestOutputHelperAccessor)}.{nameof(ITestOutputHelperAccessor.TestOutput)} is null."); }
                _accessor.TestOutput.WriteLine(message);
            }
            else
            {
                _output.WriteLine(message);
            }
        }

        public XunitTestLoggerProvider Provider => _provider;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}
