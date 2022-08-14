using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogAssignedPersonNameSpecification : BaseSpecification<FeedLog>
{
    public FeedLogAssignedPersonNameSpecification(string assignedPersonName)
    {
        if (string.IsNullOrEmpty(assignedPersonName))
        {
            FilterCondition = p => true;
        }
        else
        {
            assignedPersonName = assignedPersonName.ToLower();
            FilterCondition = p => p.AssignedPersonName.ToLower().Contains(assignedPersonName);
        }
    }
}