using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogAssetLocationSpecification : BaseSpecification<FeedLog>
{
    public FeedLogAssetLocationSpecification(string assetLocation)
    {
        if (string.IsNullOrEmpty(assetLocation))
        {
            FilterCondition = p => true;
        }
        else
        {
            assetLocation = assetLocation.ToLower();
            FilterCondition = p => p.AssetLocation.ToLower().Contains(assetLocation);
        }
    }
}