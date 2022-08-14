using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogAttendStatusSpecification : BaseSpecification<FeedLog>
{
    public FeedLogAttendStatusSpecification(LogAttendStatus? logAttendStatus)
    {
        if (logAttendStatus is null)
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p => p.LogAttendStatus == logAttendStatus.Value;
        }
    }
}