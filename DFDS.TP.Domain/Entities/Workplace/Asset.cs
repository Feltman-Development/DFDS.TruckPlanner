using DFDS.TP.Domain.Base;

namespace DFDS.TP.Domain.Entities.Workplace;

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