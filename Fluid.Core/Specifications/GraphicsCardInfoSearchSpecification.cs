using Fluid.Core.Specifications.Base;
using Fluid.Shared.Entities;

namespace Fluid.Core.Specifications;

public class GraphicsCardInfoSearchSpecification : BaseSpecification<GraphicsCardInfo>
{
    public GraphicsCardInfoSearchSpecification(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            FilterCondition = p => true;
        }
        else
        {
            searchString = searchString.ToLower();
            FilterCondition = p => p.OemSerialNo.ToLower().Contains(searchString)
                                || p.Manufacturer.ToLower().Contains(searchString)
                                || p.Model.ToLower().Contains(searchString)
                                || p.Price.ToString().Contains(searchString);
        }
    }
}
