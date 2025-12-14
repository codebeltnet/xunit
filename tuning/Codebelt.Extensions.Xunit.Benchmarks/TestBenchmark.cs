using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Codebelt.Extensions.Xunit
{
    /// <summary>
    /// Benchmarks for the <see cref="Test.Match"/> method.
    /// </summary>
    [MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class TestBenchmark
    {
        [Params(8, 256, 4096)]
        public int Length { get; set; }

        private string _shortPattern;
        private string _shortActual;
        private string _wildcardPattern;
        private string _wildcardActual;
        private string _multilinePattern;
        private string _multilineActual;
        private string _complexWildcardPattern;
        private string _complexWildcardActual;

        [GlobalSetup]
        public void Setup()
        {
            // Simple exact match - baseline
            _shortPattern = "test-string";
            _shortActual = "test-string";

            // Simple wildcard match
            _wildcardPattern = "test-*";
            _wildcardActual = "test-string";

            // Multiline pattern matching
            var lines = new string[Length / 10];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = $"Line {i}: Some content here";
            }
            _multilinePattern = string.Join(Environment.NewLine, lines);
            _multilineActual = string.Join(Environment.NewLine, lines);

            // Complex wildcard with multiple wildcards
            _complexWildcardPattern = "prefix-*-middle-?-suffix";
            _complexWildcardActual = "prefix-some-long-text-middle-X-suffix";
        }

        [Benchmark(Baseline = true, Description = "Match - exact string")]
        public bool Match_Exact()
        {
            return Test.Match(_shortPattern, _shortActual);
        }

        [Benchmark(Description = "Match - simple wildcard")]
        public bool Match_SimpleWildcard()
        {
            return Test.Match(_wildcardPattern, _wildcardActual);
        }

        [Benchmark(Description = "Match - multiline")]
        public bool Match_Multiline()
        {
            return Test.Match(_multilinePattern, _multilineActual);
        }

        [Benchmark(Description = "Match - complex wildcard")]
        public bool Match_ComplexWildcard()
        {
            return Test.Match(_complexWildcardPattern, _complexWildcardActual);
        }
    }
}
