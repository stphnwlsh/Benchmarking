namespace Benchmarking.Any.Array;

using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Models.Person;

[RankColumn]
[MemoryDiagnoser]
public class SinglePersonMatchAny
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
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkArray[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkArray.Where(p => p.DateOfBirth == fixedDateOfBirth).Any();
    }

    [Benchmark]
    public void WhereCount()
    {
        var benchmarkArray = this.array;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkArray[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkArray.Where(p => p.DateOfBirth == fixedDateOfBirth).Count() > 0;
    }

    [Benchmark]
    public void Any()
    {
        var benchmarkArray = this.array;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkArray[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkArray.Any(p => p.DateOfBirth == fixedDateOfBirth);
    }

    [Benchmark]
    public void FirstOrDefaultArray()
    {
        var benchmarkArray = this.array;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkArray[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkArray.FirstOrDefault(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }

    [Benchmark]
    public void FirstArray()
    {
        var benchmarkArray = this.array;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkArray[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkArray.First(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }

    [Benchmark]
    public void SingleArray()
    {
        var benchmarkArray = this.array;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkArray[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkArray.Single(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }

    [Benchmark]
    public void SingleOrDefaultArray()
    {
        var benchmarkArray = this.array;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkArray[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkArray.SingleOrDefault(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }
}
