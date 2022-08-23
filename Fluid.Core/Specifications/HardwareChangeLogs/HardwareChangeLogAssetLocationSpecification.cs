using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogAssetLocationSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogAssetLocationSpecification(string assetLocation)
    {
        if (string.IsNullOrEmpty(assetLocation))
        {
            FilterCondition = p => true;
        }
        else
        {
            assetLocation = assetLocation.ToLower();
            FilterCondition = p => p.AssetLocation.ToLower().Contains(assetLocation) || p.OldAssetLocation.ToLower().Contains(assetLocation);
        }
    }
}