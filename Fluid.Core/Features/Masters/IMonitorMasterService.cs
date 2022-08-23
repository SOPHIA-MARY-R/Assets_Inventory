using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMonitorMasterService
    {
        Task<Result<string>> AddAsync(MonitorInfo monitorInfo);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(MonitorInfo monitorInfo);
        Task<PaginatedResult<MonitorInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MonitorInfo>> GetByIdAsync(string oemSerialNo);
    }
}