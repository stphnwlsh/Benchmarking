namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class IteratingArrayFors
    {
        [Params(1000000)]
        public int N;

        private string[] array;

        [GlobalSetup]
        public void Setup()
        {
            this.array = new string[this.N];

            for (var i = 0; i < this.N; i++)
            {
                this.array[i] = $"i";
            }
        }

        [Benchmark]
        public void ForLoopLocalLength()
        {
            var benchmarkArray = this.array;
            var length = benchmarkArray.Length;

            for (var i = 0; i < length; i++)
            {
                var allocated = $"{benchmarkArray[i]}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void SpanForLoop()
        {
            var benchmarkSpan = this.array.AsSpan();

            for (var i = 0; i < this.N; i++)
            {
                var allocated = $"{benchmarkSpan[i]}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void SpanForLoopLocalLength()
        {
            var benchmarkSpan = this.array.AsSpan();
            var length = benchmarkSpan.Length;

            for (var i = 0; i < length; i++)
            {
                var allocated = $"{benchmarkSpan[i]}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void ForEachLoop()
        {
            var benchmarkArray = this.array;

            foreach (var item in benchmarkArray)
            {
                var allocated = $"{item}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }

        [Benchmark]
        public void SpanForEachLoop()
        {
            var benchmarkSpan = this.array.AsSpan();

            foreach (var item in benchmarkSpan)
            {
                var allocated = $"{item}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
            }
        }
    }
}
