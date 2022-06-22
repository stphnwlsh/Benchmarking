namespace Benchmarking
{
    using System;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class IteratingArrayFors
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
        public void ForLoopLocalArray()
        {
            var local = this.benchmarkArray;

            for (var i = 0; i < this.N; i++)
            {
                var allocated = $"{local[i]}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void ForLoopLocalLength()
        {
            var local = this.benchmarkArray.Length;

            for (var i = 0; i < local; i++)
            {
                var allocated = $"{this.benchmarkArray[i]}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }
        [Benchmark]
        public void ForLoopLocalArrayLength()
        {
            var localArray = this.benchmarkArray;
            var localLength = this.benchmarkArray.Length;

            for (var i = 0; i < localLength; i++)
            {
                var allocated = $"{localArray[i]}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }
    }
}
