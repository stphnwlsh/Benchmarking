namespace Benchmarking.Any.ListObject
{
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class AllMatchListObjectAny
    {
        [Params(100, 10000)]
        public int N;

        private List<Test> list = new List<Test>();

        [GlobalSetup]
        public void Setup()
        {
            // Fill List with Random Numbers (0-99)
            for (var i = 0; i < this.N; i++)
            {
                this.list.Add(new Test($"100"));
            }
        }

        [Benchmark]
        public void WhereAny()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Where(item => item.Value == "100").Any();
        }

        [Benchmark]
        public void WhereCount()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Where(item => item.Value == "100").Count() > 0;
        }

        [Benchmark]
        public void Any()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Any(item => item.Value == "100");
        }

        [Benchmark]
        public void Count()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Count(item => item.Value == "100") > 0;
        }
    }
}
