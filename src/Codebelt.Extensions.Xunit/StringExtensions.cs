#if NETSTANDARD2_0_OR_GREATER
using System;
using System.Text.RegularExpressions;

namespace Codebelt.Extensions.Xunit
{
    /// <summary>
    /// Extension methods for the <see cref="string" /> class.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex NewLineRegex = new(@"\r\n|\r|\n", RegexOptions.Compiled);

        /// <summary>
        /// Replaces all newline sequences in the current string with <see cref="Environment.NewLine"/>.
        /// </summary>
        /// <param name="input">The <see cref="string"/> to extend.</param>
        /// <returns>A string whose contents match the current string, but with all newline sequences replaced with <see cref="Environment.NewLine"/>.</returns>
        /// <remarks>Shamefully stolen from https://github.com/WebFormsCore/WebFormsCore/blob/main/src/WebFormsCore/Util/StringExtensions.cs to support .NET Standard 2.0.</remarks>
        public static string ReplaceLineEndings(this string input)
        {
            return ReplaceLineEndings(input, Environment.NewLine);
        }

        /// <summary>
        /// Replaces all newline sequences in the current string with <paramref name="replacementText"/>.
        /// </summary>
        /// <param name="input">The <see cref="string"/> to extend.</param>
        /// <param name="replacementText">The text to use as replacement.</param>
        /// <returns>A string whose contents match the current string, but with all newline sequences replaced with <paramref name="replacementText"/>.</returns>
        /// <remarks>Shamefully stolen from https://github.com/WebFormsCore/WebFormsCore/blob/main/src/WebFormsCore/Util/StringExtensions.cs to support .NET Standard 2.0.</remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="input"/> cannot be null -or-
        /// <paramref name="replacementText"/> cannot be null.
        /// </exception>
        public static string ReplaceLineEndings(this string input, string replacementText)
        {
            if (input == null) { throw new ArgumentNullException(nameof(input)); }
            if (replacementText == null) { throw new ArgumentNullException(nameof(replacementText)); }
            return NewLineRegex.Replace(input, replacementText);
        }
    }
}
#endif
