namespace DFDS.TP.Domain.Entities.Workplace;

[Flags]
public enum AssetType
{
    Cash = 1,
    Stock = 2,
    AccountsReceivable = 4,
    Inventory = 8,
    Investment = 16,
    PropertyPlantEquipment = 32,
    Vehicles = 64,
    Furniture = 128,
    
    Tangible = 256,
    Intangible = 512,
    Current = 1024,
    NonCurrent = 2048,
    Operating = 4096,
    NonOperating = 8192,
}