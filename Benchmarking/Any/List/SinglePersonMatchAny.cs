namespace Benchmarking.Any.List;

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Models.Person;

[ShortRunJob]
[RankColumn]
[MemoryDiagnoser]
public class SinglePersonMatchAny
{
    [Params(10000)]
    public int PeopleCount;

    [Params(2, 5, 10, 25, 50, 75, 90, 95, 98)]
    public int Percentage;

    private List<Person> list = PersonExtensions.GetPeopleList(10000);

    [Benchmark]
    public void WhereAnyList()
    {
        var benchmarkList = this.list;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkList[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkList.Where(p => p.DateOfBirth == fixedDateOfBirth).Any();
    }

    [Benchmark]
    public void WhereCountList()
    {
        var benchmarkList = this.list;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkList[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkList.Where(p => p.DateOfBirth == fixedDateOfBirth).Count() > 0;
    }

    [Benchmark]
    public void AnyList()
    {
        var benchmarkList = this.list;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkList[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkList.Any(p => p.DateOfBirth == fixedDateOfBirth);
    }

    [Benchmark]
    public void FirstOrDefaultList()
    {
        var benchmarkList = this.list;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkList[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkList.FirstOrDefault(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }

    [Benchmark]
    public void FirstList()
    {
        var benchmarkList = this.list;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkList[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkList.First(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }

    [Benchmark]
    public void SingleList()
    {
        var benchmarkList = this.list;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkList[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkList.Single(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }

    [Benchmark]
    public void SingleOrDefaultList()
    {
        var benchmarkList = this.list;
        var fixedDateOfBirth = DateTime.Now.AddYears(1);
        var index = (int)(this.PeopleCount * (((decimal)this.Percentage) / 100));

        benchmarkList[index].DateOfBirth = fixedDateOfBirth;

        _ = benchmarkList.SingleOrDefault(p => p.DateOfBirth == fixedDateOfBirth) != null;
    }
}
