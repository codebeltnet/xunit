﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit.Hosting
{
    internal sealed class XunitTestLoggerProvider : InMemoryTestStore<XunitTestLoggerEntry>, ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, XunitTestLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);
        private readonly ITestOutputHelperAccessor _accessor;
        private readonly ITestOutputHelper _output;

        public XunitTestLoggerProvider()
        {
        }

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

        public ITestStore<XunitTestLoggerEntry> this[string categoryName]
        {
            get
            {
                if (_loggers.TryGetValue(categoryName, out var logger))
                {
                    return logger;
                }
                throw new KeyNotFoundException($"Logger for category '{categoryName}' not found.");
            }
        }

        public void Dispose()
        {
        }
    }
}
