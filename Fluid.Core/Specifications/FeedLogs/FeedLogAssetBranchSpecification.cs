using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogAssetBranchSpecification : BaseSpecification<FeedLog>
{
    public FeedLogAssetBranchSpecification(string assetBranch)
    {
        if (string.IsNullOrEmpty(assetBranch))
        {
            FilterCondition = p => true;
        }
        else
        {
            assetBranch = assetBranch.ToLower();
            FilterCondition = p => p.AssetBranch.ToLower().Contains(assetBranch);
        }
    }
}