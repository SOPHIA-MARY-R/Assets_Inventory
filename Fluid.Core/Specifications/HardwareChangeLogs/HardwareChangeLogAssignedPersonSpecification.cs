using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications.HardwareChangeLogs;

public sealed class HardwareChangeLogAssignedPersonSpecification : BaseSpecification<HardwareChangeLog>
{
    public HardwareChangeLogAssignedPersonSpecification(string assignedPerson)
    {
        if (string.IsNullOrEmpty(assignedPerson))
        {
            FilterCondition = p => true;
        }
        else
        {
            assignedPerson = assignedPerson.ToLower();
            FilterCondition = p => p.AssignedPersonName.ToLower()
                                       .Contains(assignedPerson) ||
                                   p.OldAssignedPersonName.ToLower()
                                       .Contains(assignedPerson);
        }
    }
}