using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class KeyboardInfoSearchSpecification : BaseSpecification<KeyboardInfo>
{
    public KeyboardInfoSearchSpecification(string searchString)
    {
        searchString = searchString.ToLower();
        if (string.IsNullOrEmpty(searchString))
        {
            FilterCondition = p => true;
        }
        else
        {
            FilterCondition = p => p.OemSerialNo.ToLower().Contains(searchString)
                                || p.Manufacturer.ToLower().Contains(searchString)
                                || p.Model.ToLower().Contains(searchString)
                                || p.Price.ToString().Contains(searchString);
        }
    }
}
