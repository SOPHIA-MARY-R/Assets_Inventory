using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogMachineNameSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogMachineNameSpecification(string machineName)
    {
        if (string.IsNullOrEmpty(machineName))
        {
            FilterCondition = p => true;
        }
        else
        {
            machineName = machineName.ToLower();
            FilterCondition = p => p.MachineName.ToLower()
                                       .Contains(machineName) ||
                                   p.OldMachineName.ToLower()
                                       .Contains(machineName);
        }
    }
}