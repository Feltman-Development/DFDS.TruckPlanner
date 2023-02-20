using DFDS.TP.Domain.Entities.Identity;

namespace DFDS.TP.Domain.Entities.Workplace;

public class TruckDriver : Employee
{
    public static TruckDriver HireTruckDriver( string firstName, string lastName, DateOnly dateOfBirth, DateOnly dateOfHire, decimal monthlySalary, int departmentId)
        => new TruckDriver(dateOfHire, firstName, lastName, dateOfBirth) { MonthlyBaseSalary = monthlySalary, DepartmentId = departmentId, EmployeeId = GetEmployeeId() };

    private TruckDriver(DateOnly dateOfHire, string firstName, string lastName, DateOnly birthday) : base(dateOfHire, firstName, lastName, birthday)
    {
    }

    /// <inheritdoc />
    public override string Title => "Truck Driver";

    /// <inheritdoc />
    public override string JobDescription => "Drive a truck. Safely and effectively, following the guidance of the Truck Plans";
}

/// <inheritdoc />
public abstract class Employee : Person
{
    /// <inheritdoc />
    protected Employee(DateOnly dateOfHire, string firstName, string lastName, DateOnly birthday) : base(firstName, lastName, birthday)
    {
        DateOfHire = dateOfHire;
    }

    /// <summary>
    /// Get the employee job title.
    /// </summary>
    public abstract string Title { get; }  
    
    /// <summary>
    /// Get the description of what the job entails.
    /// </summary>
    public abstract string JobDescription { get; }
    
    public int EmployeeId { get; set; }

    public int DepartmentId { get; set; }
    
    public DateOnly DateOfHire { get; set; }

    public decimal MonthlyBaseSalary { get; set; }

    protected static int GetEmployeeId() => HighestAssignedEmployeeId +=1;

    private static int HighestAssignedEmployeeId { get; set; }
}