namespace Benchmarking.Any.Archive;

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;

[RankColumn]
[MemoryDiagnoser]
[MarkdownExporterAttribute.Default]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class RandomMatchArrayAny
{
    [Params(10000)]
    public int N;

    private int[] array;

    [GlobalSetup]
    public void Setup()
    {
        // Initialise Array
        this.array = new int[this.N];

        var random = new Random();

        // Fill Array with Random Numbers (0-99)
        for (var i = 0; i < this.N; i++)
        {
            this.array[i] = random.Next(0, 99);
        }
    }

    [Benchmark]
    public void WhereAny()
    {
        var benchmarkArray = this.array;

        _ = benchmarkArray.Where(item => item > 49).Any();
    }

    [Benchmark]
    public void WhereCount()
    {
        var benchmarkArray = this.array;

        _ = benchmarkArray.Where(item => item > 49).Count() > 0;
    }

    [Benchmark]
    public void Any()
    {
        var benchmarkArray = this.array;

        _ = benchmarkArray.Any(item => item > 49);
    }

    [Benchmark]
    public void Count()
    {
        var benchmarkArray = this.array;

        _ = benchmarkArray.Count(item => item > 49) > 0;
    }
}