using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogModelSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogModelSpecification(string model)
    {
        if (string.IsNullOrEmpty(model))
        {
            FilterCondition = p => true;
        }
        else
        {
            model = model.ToLower();
            FilterCondition = p => p.Model.ToLower().Contains(model);
        }
    }
}