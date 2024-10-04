using System;

namespace Codebelt.Extensions.Xunit.Hosting.AspNetCore
{
    internal static class Tweaker
    {
        internal static T Adjust<T>(T value, Func<T, T> converter)
        {
            return converter == null ? value : converter.Invoke(value);
        }
    }
}
