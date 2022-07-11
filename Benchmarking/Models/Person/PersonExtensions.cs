namespace Benchmarking.Models.Person;

using System;
using System.Collections.Generic;
using Bogus;

public static class PersonExtensions
{
    public static List<Person> GetPeopleList(int count = 10000)
    {
        var userFaker = new Faker<Person>("en_AU")
            .RuleFor(p => p.Id, Guid.NewGuid())
            .RuleFor(o => o.FirstName, f => f.Name.FirstName())
            .RuleFor(o => o.LastName, f => f.Name.LastName())
            .RuleFor(o => o.DateOfBirth, f => f.Date.Past(100))
            .RuleFor(o => o.Phone, f => f.Person.Phone)
            .RuleFor(o => o.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName));

        var users = userFaker.Generate(count);

        return users;
    }

    public static Person[] GetPeopleArray(int count)
    {
        return GetPeopleList(count).ToArray();
    }
}