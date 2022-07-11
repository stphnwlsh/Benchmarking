namespace Benchmarking.Models
{
    using System;

    public record Person(
        Guid Id,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Phone,
        string Email
        );
}
