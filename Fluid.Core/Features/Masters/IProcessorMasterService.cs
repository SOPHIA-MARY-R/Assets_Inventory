using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IProcessorMasterService
    {
        Task<Result<string>> AddAsync(ProcessorModel model);
        Task<Result<string>> DeleteAsync(string processorId);
        Task<Result<string>> EditAsync(ProcessorModel model);
        Task<PaginatedResult<ProcessorModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<ProcessorInfo>> GetByIdAsync(string processorId);
    }
}