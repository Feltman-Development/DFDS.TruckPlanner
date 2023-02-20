using DFDS.TP.Domain.Entities.Identity;

namespace DFDS.TP.Domain.Entities.Workplace;

/// <inheritdoc />
public abstract class Employee : Person
{
    /// <inheritdoc />
    protected Employee(DateOnly dateOfHire, string firstName, string lastName, DateOnly birthday, string title, string jobDescription, decimal salary) : base(firstName, lastName, birthday)
    {
        DateOfHire = dateOfHire;
        Title = title;
        JobDescription = jobDescription;
        MonthlyBaseSalary = salary;
    }

    /// <summary>
    /// Get the employee job title.
    /// </summary>
    public string Title { get; init; }  
    
    /// <summary>
    /// Get the description of what the job entails.
    /// </summary>
    public string JobDescription { get; init; }
    
    public int EmployeeId { get; set; }

    public int DepartmentId { get; set; }
    
    public DateOnly DateOfHire { get; set; }

    public decimal MonthlyBaseSalary { get; init; }

    protected static int GetEmployeeId() => HighestAssignedEmployeeId +=1;

    private static int HighestAssignedEmployeeId { get; set; }
}