using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;

namespace Codebelt.Extensions.Xunit;

/// <summary>
/// Benchmarks the consumer-visible querying cost of <see cref="InMemoryTestStore{T}"/>.
/// </summary>
/// <remarks>
/// Two questions are answered:
/// <list type="number">
/// <item>How does <see cref="InMemoryTestStore{T}.Query"/> with a predicate scale with store size and selectivity when consumed by a terminal <c>Count()</c>?</item>
/// <item>Does <see cref="InMemoryTestStore{T}.QueryFor{TResult}"/> add measurable overhead beyond the equivalent lean type-equality predicate, when both terminate in <c>Count()</c> and return the same count?</item>
/// </list>
/// </remarks>
[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class InMemoryTestStoreBenchmark
{
    [Params(256, 4096, 65536)]
    public int Count { get; set; }

    private InMemoryTestStore<string> _stringStore;
    private InMemoryTestStore<ItemBase> _mixedStore;

    private int _expectedAllMatch;
    private int _expectedHalfMatch;
    private int _expectedNoMatch;
    private int _expectedDerived1Count;

    [GlobalSetup]
    public void Setup()
    {
        _stringStore = new InMemoryTestStore<string>();
        for (int i = 0; i < Count; i++)
        {
            _stringStore.Add("item-" + i.ToString("D5"));
        }

        _expectedAllMatch = Count;
        _expectedHalfMatch = 0;
        for (int i = 0; i < Count; i++)
        {
            int lastDigit = i % 10;
            if (lastDigit % 2 == 0)
            {
                _expectedHalfMatch++;
            }
        }
        _expectedNoMatch = 0;

        if (_stringStore.Query(x => x.Length == 10).Count() != _expectedAllMatch)
        {
            throw new InvalidOperationException("AllMatch oracle mismatch");
        }
        if (_stringStore.Query(IsLastDigitEven).Count() != _expectedHalfMatch)
        {
            throw new InvalidOperationException("HalfMatch oracle mismatch");
        }
        if (_stringStore.Query(x => x.Length > 100).Count() != _expectedNoMatch)
        {
            throw new InvalidOperationException("NoMatch oracle mismatch");
        }

        _mixedStore = new InMemoryTestStore<ItemBase>();
        for (int i = 0; i < Count; i++)
        {
            _mixedStore.Add(i % 2 == 0 ? new Derived1() : new Derived2());
        }
        _expectedDerived1Count = (Count + 1) / 2;

        if (_mixedStore.Query(item => item.GetType() == typeof(Derived1)).Count() != _expectedDerived1Count)
        {
            throw new InvalidOperationException("Lean predicate Derived1 oracle mismatch");
        }
        if (_mixedStore.QueryFor<Derived1>().Count() != _expectedDerived1Count)
        {
            throw new InvalidOperationException("QueryFor<Derived1> oracle mismatch");
        }
    }

    [BenchmarkCategory("Query")]
    [Benchmark(Description = "Query(predicate).Count() - all items match")]
    public int Query_Predicate_AllMatch()
    {
        return _stringStore.Query(x => x.Length == 10).Count();
    }

    [BenchmarkCategory("Query")]
    [Benchmark(Description = "Query(predicate).Count() - half items match")]
    public int Query_Predicate_HalfMatch()
    {
        return _stringStore.Query(IsLastDigitEven).Count();
    }

    [BenchmarkCategory("Query")]
    [Benchmark(Description = "Query(predicate).Count() - no items match")]
    public int Query_Predicate_NoMatch()
    {
        return _stringStore.Query(x => x.Length > 100).Count();
    }

    [BenchmarkCategory("QueryFor")]
    [Benchmark(Baseline = true, Description = "Query(type-equality predicate).Count()")]
    public int Query_LeanTypePredicate()
    {
        return _mixedStore.Query(item => item.GetType() == typeof(Derived1)).Count();
    }

    [BenchmarkCategory("QueryFor")]
    [Benchmark(Description = "QueryFor<Derived1>().Count()")]
    public int QueryFor_TypeFilter()
    {
        return _mixedStore.QueryFor<Derived1>().Count();
    }

    private static bool IsLastDigitEven(string value)
    {
        return (value[value.Length - 1] - '0') % 2 == 0;
    }

    private abstract class ItemBase
    {
    }

    private sealed class Derived1 : ItemBase
    {
    }

    private sealed class Derived2 : ItemBase
    {
    }
}