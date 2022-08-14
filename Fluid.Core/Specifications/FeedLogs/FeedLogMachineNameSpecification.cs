using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogMachineNameSpecification : BaseSpecification<FeedLog>
{
    public FeedLogMachineNameSpecification(string machineName)
    {
        if (string.IsNullOrEmpty(machineName))
        {
            FilterCondition = p => true;
        }
        else
        {
            machineName = machineName.ToLower();
            FilterCondition = p => p.MachineName.ToLower().Contains(machineName);
        }
    }
}