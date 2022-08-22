﻿using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class HardDiskInfoAssetTagSpecification : BaseSpecification<HardDiskInfo>
{
    public HardDiskInfoAssetTagSpecification(string assetTag)
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