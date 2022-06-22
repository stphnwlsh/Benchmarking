namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Jobs;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class IteratingLists
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

                if (allocated == string.Empty)
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void ForEach()
        {
            foreach (var item in this.benchmarkList)
            {
                var allocated = $"{item}";

                if (allocated == string.Empty)
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void ForEachLinq()
        {
            this.benchmarkList.ForEach(item =>
            {
                var allocated = $"{item}";

                if (allocated == string.Empty)
                {
                    Console.WriteLine(allocated);
                };
            });
        }

        [Benchmark]
        public void GetEnumerator()
        {
            var enumerator = this.benchmarkList.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var allocated = $"{enumerator.Current}";

                if (allocated == string.Empty)
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void SpanFor()
        {
            var span = CollectionsMarshal.AsSpan(this.benchmarkList);

            for (var i = 0; i < this.N; i++)
            {
                var allocated = $"{span[i]}";

                if (allocated == string.Empty)
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void SpanForEach()
        {
            var span = CollectionsMarshal.AsSpan(this.benchmarkList);

            foreach (var item in span)
            {
                var allocated = $"{item}";

                if (allocated == string.Empty)
                {
                    Console.WriteLine(allocated);
                };
            }
        }
    }
}
