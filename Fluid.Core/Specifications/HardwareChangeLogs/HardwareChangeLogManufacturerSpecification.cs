using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogManufacturerSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogManufacturerSpecification(string manufacturer)
    {
        if (string.IsNullOrEmpty(manufacturer))
        {
            FilterCondition = p => true;
        }
        else
        {
            manufacturer = manufacturer.ToLower();
            FilterCondition = p => p.Manufacturer.ToLower().Contains(manufacturer);
        }
    }
}