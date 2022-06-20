namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
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
                _ = this.benchmarkArray[i];
            }
        }

        [Benchmark]
        public void ForEachLoop()
        {
            foreach (var item in this.benchmarkArray)
            {
                _ = item;
            }
        }

        [Benchmark]
        public void ArrayForEach()
        {
            Array.ForEach(this.benchmarkArray, item => _ = item);
        }

        [Benchmark]
        public void GetEnumerator()
        {
            var enumerator = this.benchmarkArray.GetEnumerator();

            while (enumerator.MoveNext())
            {
                _ = enumerator.Current;
            }
        }

        // [Benchmark]
        // public void SpanFor()
        // {
        //     var span = this.benchmarkArray.AsSpan();

        //     for (var i = 0; i < this.N; i++)
        //     {
        //         _ = span[i];
        //     }
        // }

        // [Benchmark]
        // public void SpanForEach()
        // {
        //     var span = this.benchmarkArray.AsSpan();

        //     foreach (var item in span)
        //     {
        //         _ = item;
        //     }
        // }
    }
}
