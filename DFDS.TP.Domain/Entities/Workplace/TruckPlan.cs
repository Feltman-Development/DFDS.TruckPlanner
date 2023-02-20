using DFDS.TP.Domain.Base;

namespace DFDS.TP.Domain.Entities.Workplace;

/// <summary>
/// A plan for a truck route
/// </summary>
public class TruckPlan : Entity
{
    public Truck Truck { get; set; }

    public TruckDriver Driver { get; set; }
    
}