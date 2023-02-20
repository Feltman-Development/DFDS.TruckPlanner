namespace DFDS.TP.Domain.Entities.Workplace;

public class GenericEmployee : Employee
{
    public static GenericEmployee HireEmployee(string firstName, string lastName, DateOnly dateOfBirth, DateOnly dateOfHire, decimal monthlySalary, int departmentId, string title, string jobDescription)
        => new GenericEmployee(dateOfHire, firstName, lastName, dateOfBirth, title, jobDescription, monthlySalary) { DepartmentId = departmentId, EmployeeId = GetEmployeeId() };

    /// <inheritdoc />
    public GenericEmployee(DateOnly dateOfHire, string firstName, string lastName, DateOnly birthday, string title, string jobDescription, decimal monthlySalary) 
        : base(dateOfHire, firstName, lastName, birthday, title, jobDescription, monthlySalary)
    {
    }
}