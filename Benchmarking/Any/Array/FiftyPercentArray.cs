namespace Benchmarking.Any.Array
{
    using System;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Order;
    using Benchmarking.Models;

    [ShortRunJob]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    [MemoryDiagnoser]
    public class FiftyPercentArray
    {
        [Params(10000)]
        public int N;

        private DateTime bornAfter;

        private Person[] array;

        [GlobalSetup]
        public void Setup()
        {
            // Set integer for 50% chance
            this.bornAfter = DateTime.Now.AddYears(-50);

            // Get Array of People
            this.array = PersonExtensions.GetPeopleArray(this.N);
        }

        [Benchmark]
        public void WhereAny()
        {
            var benchmarkArray = this.array;

            _ = benchmarkArray.Where(item => item.DateOfBirth > this.bornAfter).Any();
        }

        [Benchmark]
        public void WhereCount()
        {
            var benchmarkArray = this.array;

            _ = benchmarkArray.Where(item => item.DateOfBirth > this.bornAfter).Count() > 0;
        }

        [Benchmark]
        public void Any()
        {
            var benchmarkArray = this.array;

            _ = benchmarkArray.Any(item => item.DateOfBirth > this.bornAfter);
        }

        [Benchmark]
        public void Count()
        {
            var benchmarkArray = this.array;

            _ = benchmarkArray.Count(item => item.DateOfBirth > this.bornAfter) > 0;
        }
    }
}
