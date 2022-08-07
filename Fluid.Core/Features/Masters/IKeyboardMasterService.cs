using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IKeyboardMasterService
    {
        Task<Result<string>> AddAsync(KeyboardModel model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(KeyboardModel model);
        Task<PaginatedResult<KeyboardModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<KeyboardInfo>> GetByIdAsync(string oemSerialNo);
    }
}