using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogMachineTypeSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogMachineTypeSpecification(MachineType? machineType)
    {
        if (machineType == null)
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p => p.MachineType == machineType;
        }
    }
}