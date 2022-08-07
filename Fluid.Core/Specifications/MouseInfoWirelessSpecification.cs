using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class MouseInfoWirelessSpecification : BaseSpecification<MouseInfo>
{
    public MouseInfoWirelessSpecification(bool isWireless)
    {
        FilterCondition = p => p.IsWireless == isWireless;
    }
}