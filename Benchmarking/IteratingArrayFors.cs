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
        [Params(10000)]
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
        public void ForLoop()
        {
            var benchmarkArray = this.array;

            for (var i = 0; i < this.N; i++)
            {
                var allocated = $"{benchmarkArray[i]}";

                if (allocated == "NotEquals")
                {
                    Console.WriteLine(allocated);
                };
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
            var benchmarkSpan = array.AsSpan();

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
            var benchmarkSpan = array.AsSpan();
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
    }
}
