using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IKeyboardMasterService
    {
        Task<Result<string>> AddAsync(KeyboardInfo keyboardInfo);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(KeyboardInfo keyboardInfo);
        Task<PaginatedResult<KeyboardInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<KeyboardInfo>> GetByIdAsync(string oemSerialNo);
    }
}