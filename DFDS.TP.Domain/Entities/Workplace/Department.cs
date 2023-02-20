using DFDS.TP.Domain.Base;

namespace DFDS.TP.Domain.Entities.Workplace;

/// <summary>
/// Name a title for an employee. An employee holds only one title at a time, no matter what project role(s) or privileges one hold.  
/// </summary>
public class Department : Entity
{
    public Department(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Get or set the DepartmentId
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Get the name of the title.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Get the description of the title.
    /// </summary>
    public string Description { get; set; }
}

public abstract class Asset : Entity
{
    public Asset(AssetType assetType, string assetName, decimal assetValue)
    {
        AssetType = assetType;
        AssetName = assetName;
        AssetValue = assetValue;
    }

    public AssetType AssetType { get; set; }

    public string AssetName { get; set; }

    public decimal AssetValue { get; set; }
}

public class Truck : Asset
{
    public static Truck BuyTruck() => new(AssetType.PropertyPlantEquipment | AssetType.Vehicles, "BlackBetty", 1000000);
    
    public Truck(AssetType assetType, string assetName, decimal assetValue) : base(assetType, assetName, assetValue)
    { }

}