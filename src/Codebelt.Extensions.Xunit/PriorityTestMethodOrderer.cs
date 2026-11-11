using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit.Sdk;
using Xunit.v3;

namespace Codebelt.Extensions.Xunit;

/// <summary>
/// Orders test methods by <see cref="PriorityAttribute"/>, followed by ordinal method name.
/// </summary>
public sealed class PriorityTestMethodOrderer : ITestMethodOrderer
{
    /// <summary>
    /// Orders methods from the lowest numeric priority to the highest, placing methods without a priority last.
    /// </summary>
    /// <typeparam name="TTestMethod">The type of test method.</typeparam>
    /// <param name="testMethods">The methods to order.</param>
    /// <returns>The ordered methods.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="testMethods"/> is <c>null</c>.</exception>
    public IReadOnlyCollection<TTestMethod> OrderTestMethods<TTestMethod>(IReadOnlyCollection<TTestMethod> testMethods)
        where TTestMethod : notnull, ITestMethod
    {
#if NETSTANDARD2_0
        if (testMethods == null) { throw new ArgumentNullException(nameof(testMethods)); }
#else
        ArgumentNullException.ThrowIfNull(testMethods);
#endif

        return testMethods
            .OrderBy(testMethod => !GetPriority(testMethod).HasValue)
            .ThenBy(testMethod => GetPriority(testMethod))
            .ThenBy(testMethod => testMethod.MethodName, StringComparer.Ordinal)
            .ToArray();
    }

    private static int? GetPriority(ITestMethod testMethod)
    {
        if (testMethod.Traits.TryGetValue("Priority", out var values) &&
            int.TryParse(values.FirstOrDefault(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var priority))
        {
            return priority;
        }

        return null;
    }
}
