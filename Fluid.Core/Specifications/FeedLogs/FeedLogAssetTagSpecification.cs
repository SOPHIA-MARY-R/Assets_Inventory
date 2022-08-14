using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogAssetTagSpecification : BaseSpecification<FeedLog>
{
    public FeedLogAssetTagSpecification(string assetTag)
    {
        if (string.IsNullOrEmpty(assetTag))
        {
            FilterCondition = p => true;
        }
        else
        {
            assetTag = assetTag.ToLower();
            FilterCondition = p => p.AssetTag.ToLower().Contains(assetTag);
        }
    }
}