using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMachineMasterService
    {
        Task<Result<string>> AddAsync(MachineInfo model);
        Task<Result<string>> DeleteAsync(string assetTag);
        Task<Result<string>> EditAsync(MachineInfo model);
        Task<PaginatedResult<MachineInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MachineInfo>> GetByIdAsync(string assetTag);
    }
}