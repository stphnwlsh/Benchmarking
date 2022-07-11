namespace Benchmarking.Any.Archive;

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

[RankColumn]
[MemoryDiagnoser]
[MarkdownExporterAttribute.Default]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class SingleMatchListLateIndexAny
{
    [Params(10000)]
    public int N;

    private List<int> list = new List<int>();

    [GlobalSetup]
    public void Setup()
    {
        var random = new Random();

        // Fill List with Random Numbers (0-99)
        for (var i = 0; i < this.N; i++)
        {
            this.list.Add(random.Next(0, 99));
        }

        // Set Single Match
        this.list[this.N / 10 * 9] = 100;
    }

    [Benchmark]
    public void WhereAny()
    {
        var benchmarkList = this.list;

        _ = benchmarkList.Where(item => item > 99).Any();
    }

    [Benchmark]
    public void WhereCount()
    {
        var benchmarkList = this.list;

        _ = benchmarkList.Where(item => item > 99).Count() > 0;
    }

    [Benchmark]
    public void Any()
    {
        var benchmarkList = this.list;

        _ = benchmarkList.Any(item => item > 99);
    }

    [Benchmark]
    public void Count()
    {
        var benchmarkList = this.list;

        _ = benchmarkList.Count(item => item > 99) > 0;
    }
}