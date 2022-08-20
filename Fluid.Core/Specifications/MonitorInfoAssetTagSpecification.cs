using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class MonitorInfoAssetTagSpecification : BaseSpecification<MonitorInfo>
{
    public MonitorInfoAssetTagSpecification(string assetTag)
    {
        if (string.IsNullOrEmpty(assetTag))
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p => p.MachineId == assetTag;
        }
    }
}