using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMouseMasterService
    {
        Task<Result<string>> AddAsync(MouseModel model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(MouseModel model);
        Task<PaginatedResult<MouseModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MouseInfo>> GetByIdAsync(string oemSerialNo);
    }
}