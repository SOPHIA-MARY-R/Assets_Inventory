using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class CameraInfoWirelessSpecification : BaseSpecification<CameraInfo>
{
    public CameraInfoWirelessSpecification(bool isWireless)
    {
        FilterCondition = p => p.IsWireLess == isWireless;
    }
}