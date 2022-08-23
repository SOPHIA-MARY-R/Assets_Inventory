using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class CameraInfoBuiltInMicSpecification : BaseSpecification<CameraInfo>
{
    public CameraInfoBuiltInMicSpecification(bool hasBuiltInMic)
    {
        FilterCondition = p => p.HasBuiltInMic == hasBuiltInMic;
    }
}