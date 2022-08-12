using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMachineMasterService
    {
        Task<Result<string>> AddAsync(MachineMasterModel model);
        Task<Result<string>> DeleteAsync(string assetTag);
        Task<Result<string>> EditAsync(MachineMasterModel model);
        Task<PaginatedResult<MachineMasterModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MachineInfo>> GetByIdAsync(string assetTag);
    }
}