using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class KeyboardInfoWirelessSpecification : BaseSpecification<KeyboardInfo>
{
    public KeyboardInfoWirelessSpecification(bool isWireless)
    {
        FilterCondition = p => p.IsWireless == isWireless;
    }
}