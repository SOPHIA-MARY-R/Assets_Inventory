using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class SpeakerInfoBlueToothSpecification : BaseSpecification<SpeakerInfo>
{
    public SpeakerInfoBlueToothSpecification(bool isBlueTooth)
    {
        FilterCondition = p => p.IsBlueTooth == isBlueTooth;
    }
}