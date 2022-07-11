namespace Benchmarking.Any.Array
{
    using System;
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
        public int Percentage;

        private Person[] array = PersonExtensions.GetPeopleArray(10000);

        [Benchmark]
        public void WhereAny()
        {
            var benchmarkArray = this.array;
            var bornAfter = DateTime.Now.AddYears(-this.Percentage);

            _ = benchmarkArray.Where(p => p.DateOfBirth > bornAfter).Any();
        }

        [Benchmark]
        public void WhereCount()
        {
            var benchmarkArray = this.array;
            var bornAfter = DateTime.Now.AddYears(-this.Percentage);

            _ = benchmarkArray.Where(p => p.DateOfBirth > bornAfter).Count() > 0;
        }

        [Benchmark]
        public void Any()
        {
            var benchmarkArray = this.array;
            var bornAfter = DateTime.Now.AddYears(-this.Percentage);

            _ = benchmarkArray.Any(p => p.DateOfBirth > bornAfter);
        }
    }
}
