using DFDS.TP.Domain.Base;
using GeoCoordinatePortable;

namespace DFDS.TP.Domain.Entities.Workplace;

/// <summary>
/// A plan for a truck route
/// </summary>
public class TruckPlan : Entity
{
    public Truck Truck { get; set; }

    public TruckDriver Driver { get; set; }

    public GeoCoordinate StartLocation { get; set; }
    
    public GeoCoordinate EndLocation { get; set; }

    public decimal StraightDistance => throw new NotImplementedException();

    public decimal DrivingDistance => throw new NotImplementedException();
}