using DFDS.TP.Core.Utility;

namespace DFDS.TP.Domain.Entities.Identity;

public class Person
{
    public Person(string firstName, string lastName, DateOnly birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public int Age => GetAge();

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public DateOnly BirthDate { get; init; }

    private int GetAge()
    {
        var laterDate = DateTime.Now;
        var years = laterDate.Year - BirthDate.Year;
        return BirthDate > laterDate.AddYears(-years).AsDateOnly() ? --years : years;
    }
}