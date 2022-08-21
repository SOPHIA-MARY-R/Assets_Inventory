using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMachineMasterService
    {
        Task<IResult> AddAsync(MachineInfo model);
        Task<IResult> DeleteAsync(string assetTag);
        Task<IResult> EditAsync(MachineInfo model, string assetTag);
        Task<PaginatedResult<MachineInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MachineInfo>> GetByIdAsync(string assetTag);
    }
}