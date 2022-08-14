using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;

namespace Fluid.Core.Features;

public interface IFeedLogService
{
    Task<IResult> SaveLog(SystemConfiguration systemConfiguration);

    Task<PaginatedResult<FeedLog>> GetAllAsync(int pageNumber, int pageSize, FeedLogFilter filter);

    Task<IResult> AttendLog(FeedLog feedLog);
}