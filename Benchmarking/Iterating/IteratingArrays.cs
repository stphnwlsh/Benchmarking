namespace Benchmarking.Iterating;

using System;
using BenchmarkDotNet.Attributes;

[RankColumn]
[MemoryDiagnoser]
[MarkdownExporterAttribute.Default]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class IteratingArrays
{
    [Params(100, 10000)]
    public int N;

    private string[] benchmarkArray;

    [GlobalSetup]
    public void Setup()
    {
        this.benchmarkArray = new string[this.N];

        for (var i = 0; i < this.N; i++)
        {
            this.benchmarkArray[i] = $"i";
        }
    }

    [Benchmark]
    public void ForLoop()
    {
        for (var i = 0; i < this.N; i++)
        {
            var allocated = $"{this.benchmarkArray[i]}";

            if (allocated == "NotEquals")
            {
                Console.WriteLine(allocated);
            };
        }
    }

    [Benchmark]
    public void ForEachLoop()
    {
        foreach (var item in this.benchmarkArray)
        {
            var allocated = $"{item}";

            if (allocated == "NotEquals")
            {
                Console.WriteLine(allocated);
            };
        }
    }

    [Benchmark]
    public void ArrayForEach()
    {
        Array.ForEach(this.benchmarkArray, item =>
        {
            var allocated = $"{item}";

            if (allocated == "NotEquals")
            {
                Console.WriteLine(allocated);
            };
        });
    }

    [Benchmark]
    public void GetEnumerator()
    {
        var enumerator = this.benchmarkArray.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var allocated = $"{enumerator.Current}";

            if (allocated == "NotEquals")
            {
                Console.WriteLine(allocated);
            };
        }
    }

    [Benchmark]
    public void SpanFor()
    {
        var span = this.benchmarkArray.AsSpan();

        for (var i = 0; i < this.N; i++)
        {
            var allocated = $"{span[i]}";

            if (allocated == "NotEquals")
            {
                Console.WriteLine(allocated);
            };
        }
    }

    [Benchmark]
    public void SpanForEach()
    {
        var span = this.benchmarkArray.AsSpan();

        foreach (var item in span)
        {
            var allocated = $"{item}";

            if (allocated == "NotEquals")
            {
                Console.WriteLine(allocated);
            };
        }
    }
}