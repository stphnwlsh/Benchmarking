namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class IteratingDictionaries
    {
        [Params(500, 5000, 50000, 500000)]
        public int N;

        private readonly Dictionary<Guid, string> items = new Dictionary<Guid, string>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            Console.WriteLine("Start GlobalSetup");

            for (var a = 0; a <= this.N; a++)
            {
                this.items.Add(Guid.NewGuid(), $"StringValue{a}");
            }

            Console.WriteLine("Finish GlobalSetup");
        }

        #region For

        [Benchmark]
        public int ForArrayMethod()
        {
            var i = 0;

            var items = this.items.Values.ToArray();

            for (var a = 0; a < items.Length - 1; a++)
            {
                i++;
            }

            return i;
        }

        #endregion For

        #region ForEach

        [Benchmark(Baseline = true)]
        public int ForEachBaselineMethod()
        {
            var i = 0;

            foreach (var one in this.items.Values)
            {
                i++;
            }

            return i;
        }

        [Benchmark]
        public int ForEachMethod()
        {
            var i = 0;

            var items = this.items.Values;

            foreach (var one in items)
            {
                i++;
            }

            return i;
        }

        [Benchmark]
        public int ForEachArrayMethod()
        {
            var i = 0;

            var items = this.items.Values.ToArray();

            foreach (var one in items)
            {
                i++;
            }

            return i;
        }

        #endregion ForEach
    }
}
