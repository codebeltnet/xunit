using System;
using System.Collections.Generic;
using System.Text;

namespace Codebelt.Extensions.Xunit
{
    internal static class DelimitedString
    {
        internal static string Create<T>(IEnumerable<T> source, Action<DelimitedStringOptions<T>> setup = null)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }

            var options = new DelimitedStringOptions<T>();
            setup?.Invoke(options);

            var delimitedValues = new StringBuilder();
            using (var enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    delimitedValues.Append(FormattableString.Invariant($"{options.StringConverter(enumerator.Current)}{options.Delimiter}"));
                }
            }
            return delimitedValues.Length > 0 ? delimitedValues.ToString(0, delimitedValues.Length - options.Delimiter.Length) : delimitedValues.ToString();
        }
    }
}
