using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IProcessorMasterService
    {
        Task<Result<string>> AddAsync(ProcessorInfo processorInfo);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(ProcessorInfo processorInfo);
        Task<PaginatedResult<ProcessorInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<ProcessorInfo>> GetByIdAsync(string oemSerialNo);
    }
}