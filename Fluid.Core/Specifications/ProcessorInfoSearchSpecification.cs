using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class ProcessorInfoSearchSpecification : BaseSpecification<ProcessorInfo>
{
    public ProcessorInfoSearchSpecification(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            FilterCondition = p => true;
        }
        else
        {
            searchString = searchString.ToLower();
            FilterCondition = p => p.ProcessorId.Contains(searchString)
                                || p.Manufacturer.ToLower().Contains(searchString)
                                || p.Architecture.ToString().ToLower().Contains(searchString)
                                || p.Family.ToString().Contains(searchString)
                                || p.NumberOfCores.ToString().Contains(searchString)
                                || p.NumberOfLogicalProcessors.ToString().Contains(searchString)
                                || p.MaxClockSpeed.ToString().Contains(searchString)
                                || p.ThreadCount.ToString().Contains(searchString)
                                || p.Name.ToLower().Contains(searchString)
                                || p.Price.ToString().Contains(searchString);
        }
    }
}
