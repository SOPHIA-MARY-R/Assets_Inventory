using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class PhysicalMemoryInfoAssetTagSpecification : BaseSpecification<PhysicalMemoryInfo>
{
    public PhysicalMemoryInfoAssetTagSpecification(string assetTag)
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