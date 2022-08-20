using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IHardDiskMasterService
    {
        Task<Result<string>> AddAsync(HardDiskInfo model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(HardDiskInfo model);
        Task<PaginatedResult<HardDiskInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<HardDiskInfo>> GetByIdAsync(string oemSerialNo);
    }
}