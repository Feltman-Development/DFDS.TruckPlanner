namespace DFDS.TP.Domain.Entities.Workplace;

public class GenericEmployee : Employee
{

    /// <inheritdoc />
    public GenericEmployee(string firstName, string lastName, DateOnly birthday) : base(firstName, lastName, birthday)
    {
    }

    public override string Title => "Employee";

    /// <inheritdoc />
    public override string JobDescription => "Anything and everything that needs to get done.";
}