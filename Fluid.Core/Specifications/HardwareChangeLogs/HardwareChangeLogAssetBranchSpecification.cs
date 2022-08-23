using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogAssetBranchSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogAssetBranchSpecification(string assetBranch)
    {
        if (string.IsNullOrEmpty(assetBranch))
        {
            FilterCondition = p => true;
        }
        else
        {
            assetBranch = assetBranch.ToLower();
            FilterCondition = p => p.AssetBranch.ToLower().Contains(assetBranch) || p.OldAssetBranch.ToLower().Contains(assetBranch);
        }
    }
}