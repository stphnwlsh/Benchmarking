namespace Benchmarking
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Jobs;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
    public class IteratingAccessingLists
    {
        [Params(100, 10000)]
        public int N;

        private readonly List<string> benchmarkList = new List<string>();

        [GlobalSetup]
        public void Setup()
        {
            for (var i = 0; i < this.N; i++)
            {
                this.benchmarkList.Add($"i");
            }
        }

        [Benchmark]
        public void For()
        {
            for (var i = 0; i < this.N; i++)
            {
                var allocated = $"{this.benchmarkList[i]}";
                _ = allocated;
            }
        }

        [Benchmark]
        public void ForEach()
        {
            foreach (var item in this.benchmarkList)
            {
                var allocated = $"{item}";
                _ = allocated;
            }
        }

        [Benchmark]
        public void ForEachLinq()
        {
            this.benchmarkList.ForEach(item => _ = item);
        }

        [Benchmark]
        public void GetEnumerator()
        {
            var enumerator = this.benchmarkList.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var allocated = $"{enumerator.Current}";
                _ = allocated;
            }
        }

        // [Benchmark]
        // public void SpanFor()
        // {
        //     var span = CollectionsMarshal.AsSpan(this.benchmarkList);

        //     for (var i = 0; i < this.N; i++)
        //     {
        //         var allocated = $"{span[i]}";
        //         _ = allocated;
        //     }
        // }

        // [Benchmark]
        // public void SpanForEach()
        // {
        //     var span = CollectionsMarshal.AsSpan(this.benchmarkList);

        //     foreach (var item in span)
        //     {
        //         var allocated = $"{item}";
        //         _ = allocated;
        //     }
        // }
    }
}
