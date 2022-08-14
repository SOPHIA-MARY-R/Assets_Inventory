using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.FeedLogs;

public class FeedLogDateRangeSpecification : BaseSpecification<FeedLog>
{
    public FeedLogDateRangeSpecification(DateTime fromDateTime, DateTime toDateTime)
    {
        FilterCondition = p => p.LogDateTime >= fromDateTime && p.LogDateTime <= toDateTime;
    }
}