using System;

namespace Codebelt.Extensions.Xunit
{
    internal sealed class DelimitedStringOptions<T>
    {
        internal DelimitedStringOptions()
        {
            Delimiter = ",";
            StringConverter = o => o.ToString();
        }

        internal Func<T, string> StringConverter { get; set; }

        internal string Delimiter { get; set; }
    }
}
