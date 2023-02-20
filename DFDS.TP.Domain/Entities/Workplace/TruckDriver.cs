namespace DFDS.TP.Domain.Entities.Workplace;

public class TruckDriver : Employee
{
    public static TruckDriver HireTruckDriver(string firstName, string lastName, DateOnly dateOfBirth, DateOnly dateOfHire, decimal monthlySalary, int departmentId)
        => new TruckDriver(dateOfHire, firstName, lastName, dateOfBirth, "TruckDriver", "Drive a truck. Safely and effectively, following the guidance of the Truck Plans", monthlySalary) 
            { DepartmentId = departmentId, EmployeeId = GetEmployeeId() };

    private TruckDriver(DateOnly dateOfHire, string firstName, string lastName, DateOnly birthday, string title, string jobDescription, decimal monthlySalary) 
        : base(dateOfHire, firstName, lastName, birthday, title, jobDescription, monthlySalary)
    {
    }
}