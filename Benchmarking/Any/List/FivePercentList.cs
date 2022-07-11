namespace Benchmarking.Any.List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using BenchmarkDotNet.Order;
    using Benchmarking.Models;

    [ShortRunJob]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    [MemoryDiagnoser]
    public class FivePercentList
    {
        [Params(10000)]
        public int N;

        private DateTime bornAfter;

        private List<Person> list;

        [GlobalSetup]
        public void Setup()
        {
            // Set integer for 5% chance
            this.bornAfter = DateTime.Now.AddYears(-5);

            // Get Array of People
            this.list = PersonExtensions.GetPeopleList(this.N);
        }

        [Benchmark]
        public void WhereAny()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Where(item => item.DateOfBirth > this.bornAfter).Any();
        }

        [Benchmark]
        public void WhereCount()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Where(item => item.DateOfBirth > this.bornAfter).Count() > 0;
        }

        [Benchmark]
        public void Any()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Any(item => item.DateOfBirth > this.bornAfter);
        }

        [Benchmark]
        public void Count()
        {
            var benchmarkList = this.list;

            _ = benchmarkList.Count(item => item.DateOfBirth > this.bornAfter) > 0;
        }
    }
}
