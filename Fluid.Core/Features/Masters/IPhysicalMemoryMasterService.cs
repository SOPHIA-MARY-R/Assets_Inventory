using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IPhysicalMemoryMasterService
    {
        Task<Result<string>> AddAsync(PhysicalMemoryInfo physicalMemoryInfo);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(PhysicalMemoryInfo physicalMemoryInfo);
        Task<PaginatedResult<PhysicalMemoryInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<PhysicalMemoryInfo>> GetByIdAsync(string oemSerialNo);
    }
}