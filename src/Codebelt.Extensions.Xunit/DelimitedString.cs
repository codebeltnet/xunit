using System;
using System.Collections.Generic;
using System.Text;

namespace Codebelt.Extensions.Xunit;

internal static class DelimitedString
{
    internal static string Create<T>(IEnumerable<T> source, string delimiter)
    {
#if NETSTANDARD2_0
        if (source == null) { throw new ArgumentNullException(nameof(source)); }
        if (delimiter == null) { throw new ArgumentNullException(nameof(delimiter)); }
#else
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(delimiter);
#endif

        var sb = new StringBuilder();
        var first = true;
        foreach (var item in source)
        {
            if (!first) { sb.Append(delimiter); }
            first = false;
            sb.Append(item);
        }
        return sb.ToString();
    }
}
