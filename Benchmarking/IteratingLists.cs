namespace Benchmarking
{
    using System.Collections.Generic;
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
        [Params(100, 1000, 100000)]
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
        public void ForLoop()
        {
            for (var i = 0; i < this.N; i++)
            {
                _ = this.benchmarkList[i];
            }
        }

        [Benchmark]
        public void ForEachLoop()
        {
            foreach (var item in this.benchmarkList)
            {
                _ = item;
            }
        }

        [Benchmark]
        public void ForEachLyncLoop()
        {
            this.benchmarkList.ForEach(item => _ = item);
        }
    }
}
