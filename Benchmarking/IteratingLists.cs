namespace Benchmarking
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class IteratingLists
    {
        [Params(100, 10000)]
        public int N;

        private readonly List<string> benchmarkList = new();

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
                _ = this.benchmarkList[i];
            }
        }

        [Benchmark]
        public void ForEach()
        {
            foreach (var item in this.benchmarkList)
            {
                _ = item;
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
                _ = enumerator.Current;
            }
        }

        [Benchmark]
        public void SpanFor()
        {
            var span = CollectionsMarshal.AsSpan(this.benchmarkList);

            for (var i = 0; i < this.N; i++)
            {
                _ = span[i];
            }
        }

        [Benchmark]
        public void SpanForEach()
        {
            var span = CollectionsMarshal.AsSpan(this.benchmarkList);

            foreach (var item in span)
            {
                _ = item;
            }
        }
    }
}
