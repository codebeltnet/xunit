using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Xunit;

namespace Codebelt.Extensions.Xunit
{
    [MemoryDiagnoser]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class DelimitedStringBenchmark
    {
        [Params(8, 256, 4096)]
        public int Count { get; set; }

        private string[] _strings;
        private int[] _ints;
        private ITestOutputHelper _helper;

        [GlobalSetup]
        public void Setup()
        {
            _strings = Enumerable.Range(0, Count).Select(i => $"item-{i}").ToArray();
            _ints = Enumerable.Range(0, Count).ToArray();
            _helper = new DummyTestOutputHelper();
        }

        [Benchmark(Baseline = true, Description = "Create from string sequence via WriteLines")]
        public void Create_String_WriteLines()
        {
            TestOutputHelperExtensions.WriteLines(_helper, _strings);
        }

        [Benchmark(Description = "Create from int sequence via WriteLines<T>")]
        public void Create_Int_WriteLines()
        {
            TestOutputHelperExtensions.WriteLines(_helper, _ints);
        }

        private sealed class DummyTestOutputHelper : ITestOutputHelper
        {
            public void WriteLine(string message)
            {
                // no-op; avoid I/O during benchmark
            }

            public void WriteLine(string format, params object[] args)
            {
                // no-op; avoid I/O during benchmark
            }

            public void Write(string message)
            {
                // no-op; avoid I/O during benchmark
            }

            public void Write(string format, params object[] args)
            {
                // no-op; avoid I/O during benchmark
            }

            public string Output => string.Empty;
        }
    }
}
