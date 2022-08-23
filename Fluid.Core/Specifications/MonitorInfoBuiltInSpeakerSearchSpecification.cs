using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class MonitorInfoBuiltInSpeakerSpecification : BaseSpecification<MonitorInfo>
{
    public MonitorInfoBuiltInSpeakerSpecification(bool hasBuiltInSpeaker)
    {
        FilterCondition = p => p.HasBuiltInSpeakers == hasBuiltInSpeaker;
    }
}