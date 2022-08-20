using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMonitorMasterService
    {
        Task<Result<string>> AddAsync(MonitorInfo model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(MonitorInfo model);
        Task<PaginatedResult<MonitorInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MonitorInfo>> GetByIdAsync(string oemSerialNo);
    }
}