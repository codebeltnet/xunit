using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Codebelt.Extensions.Xunit.Hosting;

/// <summary>
/// Extension methods for the <see cref="ILogger{TCategoryName}"/> interface.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>
    /// Returns the associated <see cref="ITestStore{T}"/> that is provided when settings up services from <see cref="ServiceCollectionExtensions.AddXunitTestLogging(Microsoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Logging.LogLevel)"/> or related.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> from which to retrieve the <see cref="ITestStore{T}"/>.</param>
    /// <param name="categoryName">The category name for messages produced by the <paramref name="logger"/> -or- <c>null</c> for messages produced by all loggers.</param>
    /// <returns>Returns an implementation of <see cref="ITestStore{T}"/> with all logged entries expressed as <see cref="XunitTestLoggerEntry"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="logger"/> cannot be null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="logger"/> does not contain a test store.
    /// </exception>
    /// <exception cref="KeyNotFoundException">
    /// <paramref name="logger"/> does not contain a test store for the specified <paramref name="categoryName"/>.
    /// </exception>
    public static ITestStore<XunitTestLoggerEntry> GetTestStore(this ILogger logger, string categoryName = null)
    {
        if (logger == null) { throw new ArgumentNullException(nameof(logger)); }
        var internalLogger = GetInternalLogger(logger);
        if (internalLogger != null)
        {
            var store = FindTestStore(internalLogger, categoryName);
            if (store != null) { return store; }
        }
        throw new ArgumentException($"Logger does not contain a test store; did you remember to call {nameof(ServiceCollectionExtensions.AddXunitTestLogging)} before calling this method?", nameof(logger));
    }

    /// <summary>
    /// Returns the associated <see cref="ITestStore{T}"/> that is provided when settings up services from <see cref="ServiceCollectionExtensions.AddXunitTestLogging(Microsoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Logging.LogLevel)"/> or related.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> from which to retrieve the <see cref="ITestStore{T}"/>.</param>
    /// <returns>Returns an implementation of <see cref="ITestStore{T}"/> with all logged entries expressed as <see cref="XunitTestLoggerEntry"/>.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="logger"/> cannot be null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="logger"/> does not contain a test store.
    /// </exception>
    public static ITestStore<XunitTestLoggerEntry> GetTestStore<T>(this ILogger<T> logger)
    {
        return GetTestStore(logger, typeof(T).FullName);
    }

    private static object GetInternalLogger(ILogger logger)
    {
        var loggerType = logger.GetType();
        return loggerType.GetRuntimeFields().SingleOrDefault(fi => fi.Name == "_logger")?.GetValue(logger);
    }

    private static ITestStore<XunitTestLoggerEntry> FindTestStore(object internalLogger, string categoryName)
    {
        var internalLoggerType = internalLogger.GetType();
        var internalLoggers = internalLoggerType.GetRuntimeProperties().SingleOrDefault(pi => pi.Name == "Loggers")?.GetValue(internalLogger);
        if (internalLoggers == null) { return null; }
        foreach (var loggerInformation in (IEnumerable)internalLoggers)
        {
            var store = TryGetTestStore(loggerInformation, categoryName);
            if (store != null) { return store; }
        }
        return null;
    }

    private static ITestStore<XunitTestLoggerEntry> TryGetTestStore(object loggerInformation, string categoryName)
    {
        var loggerInformationType = loggerInformation.GetType();
        var providerType = loggerInformationType.GetProperty("ProviderType")?.GetValue(loggerInformation) as Type;
        if (providerType != typeof(XunitTestLoggerProvider)) { return null; }
        if (loggerInformationType.GetProperty("Logger")?.GetValue(loggerInformation) is not XunitTestLogger xunitTestLogger) { return null; }
        return categoryName == null
            ? xunitTestLogger.Provider
            : xunitTestLogger.Provider[categoryName];
    }
}
