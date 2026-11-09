namespace Benchmarking
{
    using System;
    using System.Threading;
    using BenchmarkDotNet.Attributes;

    /// <summary>
    /// Three thread-safe ways to get random numbers. Sharing one plain <see cref="Random"/>
    /// between threads isn't on the list, because it isn't thread-safe and quietly starts
    /// returning zeros when it breaks.
    /// </summary>
    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class GeneratingRandomNumbers
    {
        [Params(100, 10000)]
        public int N;

        private readonly Random locked = new();
        private readonly ThreadLocal<Random> perThread = new(() => new Random());

        [Benchmark(Baseline = true)]
        public long RandomShared()
        {
            long total = 0;

            for (var i = 0; i < this.N; i++)
            {
                total += Random.Shared.Next(1, int.MaxValue);
            }

            return total;
        }

        [Benchmark]
        public long LockedRandom()
        {
            long total = 0;

            for (var i = 0; i < this.N; i++)
            {
                lock (this.locked)
                {
                    total += this.locked.Next(1, int.MaxValue);
                }
            }

            return total;
        }

        [Benchmark]
        public long ThreadLocalRandom()
        {
            long total = 0;

            for (var i = 0; i < this.N; i++)
            {
                total += this.perThread.Value.Next(1, int.MaxValue);
            }

            return total;
        }
    }
}
