using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IHardDiskMasterService
    {
        Task<Result<string>> AddAsync(HardDiskModel model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(HardDiskModel model);
        Task<PaginatedResult<HardDiskModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<HardDiskInfo>> GetByIdAsync(string oemSerialNo);
    }
}