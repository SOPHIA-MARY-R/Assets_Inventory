using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogMachineTypeSpecification : BaseSpecification<FeedLog>
{
    public FeedLogMachineTypeSpecification(MachineType? machineType)
    {
        if (machineType is null)
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p => p.MachineType == machineType.Value;
        }
    }
}