﻿using System;

namespace Codebelt.Extensions.Xunit
{
    /// <summary>
    /// Represents the members needed for vanilla testing.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public interface ITest : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Gets the type of caller for this instance. Default is <see cref="object.GetType"/>.
        /// </summary>
        /// <value>The type of caller for this instance.</value>
        Type CallerType { get; }
    }
}
