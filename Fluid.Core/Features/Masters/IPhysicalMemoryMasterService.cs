using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IPhysicalMemoryMasterService
    {
        Task<Result<string>> AddAsync(PhysicalMemoryModel model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(PhysicalMemoryModel model);
        Task<PaginatedResult<PhysicalMemoryModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<PhysicalMemoryInfo>> GetByIdAsync(string oemSerialNo);
    }
}