using System;
using System.Linq;
using System.Text.RegularExpressions;
using Cuemon;
using Xunit.Abstractions;

namespace Codebelt.Extensions.Xunit
{
    /// <summary>
    /// Represents the base class from which all implementations of unit testing should derive.
    /// </summary>
    /// <seealso cref="ITestOutputHelper"/>
    public abstract class Test : ITest
    {
        /// <summary>
        /// Provides a way, with wildcard support, to determine if <paramref name="actual" /> matches <paramref name="expected" />.
        /// </summary>
        /// <param name="expected">The expected string value.</param>
        /// <param name="actual">The actual string value.</param>
        /// <param name="setup">The <see cref="WildcardOptions" /> which may be configured.</param>
        /// <returns><c>true</c> if <paramref name="actual" /> matches <paramref name="expected" />, <c>false</c> otherwise.</returns>
        /// <remarks>Credits for inspiration goes to this SO answer: https://stackoverflow.com/questions/30299671/matching-strings-with-wildcard/30300521#30300521</remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="expected"/> cannot be matched with <paramref name="actual"/>. Includes the non-matched string(s) in <see cref="ArgumentOutOfRangeException.ActualValue"/>.
        /// </exception>
        public static bool Match(string expected, string actual, Action<WildcardOptions> setup = null)
        {
            Validator.ThrowIfInvalidConfigurator(setup, out var options);

            var pattern = $"^{Regex.Escape(expected).Replace(options.SingleCharacter, ".").Replace(options.GroupOfCharacters, ".*")}$";

            if (Regex.IsMatch(actual, pattern, RegexOptions.None, TimeSpan.FromSeconds(2))) { return true; }

            var e = expected.Split(Environment.NewLine.ToCharArray());
            var a = actual.Split(Environment.NewLine.ToCharArray());
            var d = e.Except(a).Where(s => s.IndexOf(options.GroupOfCharacters, StringComparison.InvariantCulture) < 0 && s.IndexOf(options.SingleCharacter, StringComparison.InvariantCulture) < 0);

            if (options.ThrowOnNoMatch) { throw new ArgumentOutOfRangeException(nameof(expected), $"'{string.Join(Environment.NewLine, d)}'", "The expected value does not match the actual value."); }
            return false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Test" /> class.
        /// </summary>
        /// <param name="output">An implementation of the <see cref="ITestOutputHelper" /> interface.</param>
        /// <param name="callerType">The <see cref="Type"/> of caller that ends up invoking this instance.</param>
        /// <remarks><paramref name="output" /> is initialized automatically in an xUnit project.</remarks>
        protected Test(ITestOutputHelper output = null, Type callerType = null)
        {
            TestOutput = output;
            CallerType = callerType ?? GetType();
        }

        /// <summary>
        /// Gets the type of caller for this instance. Default is <see cref="object.GetType"/>.
        /// </summary>
        /// <value>The type of caller for this instance.</value>
        public Type CallerType { get; }

        /// <summary>
        /// Gets the console substitute to write out unit test information.
        /// </summary>
        /// <value>The console substitute to write out unit test information.</value>
        protected ITestOutputHelper TestOutput { get; }

        /// <summary>
        /// Gets a value indicating whether <see cref="TestOutput"/> has a reference to an implementation of <see cref="ITestOutputHelper"/>.
        /// </summary>
        /// <value><c>true</c> if this instance has has a reference to an implementation of <see cref="ITestOutputHelper"/>; otherwise, <c>false</c>.</value>
        protected bool HasTestOutput => TestOutput != null;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Test"/> object is disposed.
        /// </summary>
        /// <value><c>true</c> if this <see cref="Test"/> object is disposed; otherwise, <c>false</c>.</value>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Called when this object is being disposed by either <see cref="Dispose()" /> or <see cref="Dispose(bool)" /> having <c>disposing</c> set to <c>true</c> and <see cref="Disposed" /> is <c>false</c>.
        /// </summary>
        protected virtual void OnDisposeManagedResources()
        {
        }

        /// <summary>
        /// Called when this object is being disposed by either <see cref="Dispose()"/> or <see cref="Dispose(bool)"/> and <see cref="Disposed"/> is <c>false</c>.
        /// </summary>
        protected virtual void OnDisposeUnmanagedResources()
        {
        }

        /// <summary>
        /// Releases all resources used by the <see cref="Test"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="Test"/> object and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            if (Disposed) { return; }
            if (disposing)
            {
                OnDisposeManagedResources();
            }
            OnDisposeUnmanagedResources();
            Disposed = true;
        }
    }
}
