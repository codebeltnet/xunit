using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Codebelt.Extensions.Xunit
{
    /// <summary>
    /// Extension methods for the <see cref="ITestOutputHelper"/>.
    /// </summary>
    public static class TestOutputHelperExtensions
    {
        /// <summary>
        /// Adds a line of text per object in <paramref name="values"/> to the output.
        /// </summary>
        /// <param name="helper">The <see cref="ITestOutputHelper"/> to extend.</param>
        /// <param name="values">The values to write, per line, to the output.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="helper"/> cannot be null.
        /// </exception>
        public static void WriteLines(this ITestOutputHelper helper, params object[] values)
        {
            if (helper == null) { throw new ArgumentNullException(nameof(helper)); }
            helper.WriteLine(DelimitedString.Create(values, o => o.Delimiter = Environment.NewLine));
        }

        /// <summary>
        /// Adds a line of text per item in <paramref name="values"/> to the output.
        /// </summary>
        /// <param name="helper">The <see cref="ITestOutputHelper"/> to extend.</param>
        /// <param name="values">The values to write, per line, to the output.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="helper"/> cannot be null.
        /// </exception>
        public static void WriteLines<T>(this ITestOutputHelper helper, T[] values)
        {
            WriteLines(helper, values.AsEnumerable());
        }

        /// <summary>
        /// Adds a line of text per item in <paramref name="values"/> to the output.
        /// </summary>
        /// <param name="helper">The <see cref="ITestOutputHelper"/> to extend.</param>
        /// <param name="values">The values to write, per line, to the output.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="helper"/> cannot be null.
        /// </exception>
        public static void WriteLines<T>(this ITestOutputHelper helper, IEnumerable<T> values)
        {
            if (helper == null) { throw new ArgumentNullException(nameof(helper)); }
            helper.WriteLine(DelimitedString.Create(values, o => o.Delimiter = Environment.NewLine));
        }
    }
}
