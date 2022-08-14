using Fluid.Shared.Models;

namespace Fluid.Core.Features;

public interface IFeedLogService
{
    Task<IResult> SaveLog(SystemConfiguration systemConfiguration);
}