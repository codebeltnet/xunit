using System;
using System.Globalization;
using Xunit;

namespace Codebelt.Extensions.Xunit;

/// <summary>
/// Assigns a numeric execution priority to a test method when its class uses <see cref="PriorityTestMethodOrderer"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class PriorityAttribute : TraitAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PriorityAttribute"/> class.
    /// </summary>
    /// <param name="priority">The priority; lower values run first.</param>
    public PriorityAttribute(int priority) : base("Priority", priority.ToString(CultureInfo.InvariantCulture))
    {
        Priority = priority;
    }

    /// <summary>
    /// Gets the numeric execution priority.
    /// </summary>
    public int Priority { get; }
}
