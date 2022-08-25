using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Models.FilterModels;
using Fluid.Shared.Requests;

namespace Fluid.Core.Features;

public interface IFeedLogService
{
    Task<IResult<FeedLog>> GetById(string id);
    Task<IResult<SystemConfiguration>> SaveLog(SystemConfiguration systemConfiguration);
    Task<PaginatedResult<FeedLog>> GetAllAsync(int pageNumber, int pageSize, FeedLogFilter filter);
    Task<IResult> AutoValidateLogsAsync();
    Task<Result<FeedLogCountDetails>> GetCountDetails();
    Task<IResult> AcceptLog(string id, AcceptChangeRequest acceptChangeRequest);
    Task<IResult> IgnoreLog(string id);
}