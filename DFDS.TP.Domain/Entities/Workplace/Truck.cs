namespace DFDS.TP.Domain.Entities.Workplace;

public class Truck : Asset
{
    public static Truck BuyTruck() => new(AssetType.PropertyPlantEquipment | AssetType.Vehicles, "BlackBetty", 1000000);
    
    public Truck(AssetType assetType, string assetName, decimal assetValue) : base(assetType, assetName, assetValue)
    { }
}