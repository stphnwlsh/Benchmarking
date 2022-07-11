namespace Benchmarking.Any.List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using Benchmarking.Models;

    [RankColumn]
    [MemoryDiagnoser]
    public class ManyPersonMatchAny
    {
        [Params(10000)]
        public int PeopleCount;

        [Params(2, 5, 10, 25, 50, 75, 90, 95, 98)]
        public int BornAfter;

        private List<Person> list = PersonExtensions.GetPeopleList(10000);

        [Benchmark]
        public void WhereAny()
        {
            var benchmarkList = this.list;
            var bornAfter = DateTime.Now.AddYears(-this.BornAfter);

            _ = benchmarkList.Where(p => p.DateOfBirth > bornAfter).Any();
        }

        [Benchmark]
        public void WhereCount()
        {
            var benchmarkList = this.list;
            var bornAfter = DateTime.Now.AddYears(-this.BornAfter);

            _ = benchmarkList.Where(p => p.DateOfBirth > bornAfter).Count() > 0;
        }

        [Benchmark]
        public void Any()
        {
            var benchmarkList = this.list;
            var bornAfter = DateTime.Now.AddYears(-this.BornAfter);

            _ = benchmarkList.Any(p => p.DateOfBirth > bornAfter);
        }
    }
}
