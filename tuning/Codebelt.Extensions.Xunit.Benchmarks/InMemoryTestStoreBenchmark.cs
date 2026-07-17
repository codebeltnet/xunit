using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Codebelt.Extensions.Xunit;

/// <summary>
/// Benchmarks for the <see cref="InMemoryTestStore{T}"/> class querying operations.
/// </summary>
[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class InMemoryTestStoreBenchmark
{
    [Params(8, 256, 4096)]
    public int Count { get; set; }

    private InMemoryTestStore<string> _stringStore;
    private InMemoryTestStore<ItemBase> _mixedStore;
    private int _expectedSimpleCount;
    private int _expectedComplexCount;
    private int _expectedTypeCount;

    [GlobalSetup]
    public void Setup()
    {
        // String store for basic query benchmarks
        _stringStore = new InMemoryTestStore<string>();
        for (int i = 0; i < Count; i++)
        {
            _stringStore.Add($"item-{i}");
        }

        // Validate baseline query returns all items
        var allItems = _stringStore.Query().Count();
        if (allItems != Count)
            throw new InvalidOperationException($"Expected {Count} items, got {allItems}");

        // Simple predicate: length > 7 (excludes single-digit counts, includes multi-digit)
        _expectedSimpleCount = _stringStore.Query(x => x.Length > 7).Count();
        if (_expectedSimpleCount == 0)
            throw new InvalidOperationException("Simple predicate returned no results; adjust test data");

        // Complex predicate: contains "item" AND length >= 8
        _expectedComplexCount = _stringStore.Query(x => x.Contains("item") && x.Length >= 8).Count();

        // Mixed type store for QueryFor<TResult> benchmark
        _mixedStore = new InMemoryTestStore<ItemBase>();
        for (int i = 0; i < Count; i++)
        {
            if (i % 2 == 0)
            {
                _mixedStore.Add(new ItemDerived1 { Id = i });
            }
            else
            {
                _mixedStore.Add(new ItemDerived2 { Id = i });
            }
        }

        // Verify type filtering works
        _expectedTypeCount = _mixedStore.QueryFor<ItemDerived1>().Count();
        if (_expectedTypeCount == 0)
            throw new InvalidOperationException("QueryFor<ItemDerived1> returned no results");

        // Sanity check: approx half should be ItemDerived1 (every other item)
        var expectedApprox = Count / 2;
        if (Math.Abs(_expectedTypeCount - expectedApprox) > 2)
            throw new InvalidOperationException($"Type filter count {_expectedTypeCount} unexpected for {Count} items");
    }

    [Benchmark(Baseline = true, Description = "Query all items (no predicate)")]
    public int Query_AllItems()
    {
        var result = _stringStore.Query();
        return result.Count();
    }

    [Benchmark(Description = "Query with simple predicate")]
    public int Query_SimplePredicate()
    {
        var result = _stringStore.Query(x => x.Length > 7);
        return result.Count();
    }

    [Benchmark(Description = "Query with complex predicate")]
    public int Query_ComplexPredicate()
    {
        var result = _stringStore.Query(x => x.Contains("item") && x.Length >= 8);
        return result.Count();
    }

    [Benchmark(Description = "QueryFor<T> type filtering")]
    public int QueryFor_TypeFilter()
    {
        var result = _mixedStore.QueryFor<ItemDerived1>();
        return result.Count();
    }

    // Test item hierarchy
    private abstract class ItemBase
    {
        public int Id { get; set; }
    }

    private sealed class ItemDerived1 : ItemBase
    {
    }

    private sealed class ItemDerived2 : ItemBase
    {
    }
}
