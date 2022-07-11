namespace Benchmarking.Any.List
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using Benchmarking.Models;

    [ShortRunJob]
    [RankColumn]
    [MemoryDiagnoser]
    public class SinglePersonMatchAny
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
            var fixedDateOfBirth = DateTime.Now.AddYears(1);
            var index = (int)(this.PeopleCount * (((decimal)this.BornAfter) / 100));

            benchmarkList[index].DateOfBirth = fixedDateOfBirth;

            _ = benchmarkList.Where(p => p.DateOfBirth == fixedDateOfBirth).Any();
        }

        [Benchmark]
        public void WhereCount()
        {
            var benchmarkList = this.list;
            var fixedDateOfBirth = DateTime.Now.AddYears(1);
            var index = (int)(this.PeopleCount * (((decimal)this.BornAfter) / 100));

            benchmarkList[index].DateOfBirth = fixedDateOfBirth;

            _ = benchmarkList.Where(p => p.DateOfBirth == fixedDateOfBirth).Count() > 0;
        }

        [Benchmark]
        public void Any()
        {
            var benchmarkList = this.list;
            var fixedDateOfBirth = DateTime.Now.AddYears(1);
            var index = (int)(this.PeopleCount * (((decimal)this.BornAfter) / 100));

            benchmarkList[index].DateOfBirth = fixedDateOfBirth;

            _ = benchmarkList.Any(p => p.DateOfBirth == fixedDateOfBirth);
        }
    }
}
