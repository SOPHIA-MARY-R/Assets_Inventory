using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IGraphicsCardMasterService
    {
        Task<Result<string>> AddAsync(GraphicsCardInfo model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(GraphicsCardInfo model);
        Task<PaginatedResult<GraphicsCardInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<GraphicsCardInfo>> GetByIdAsync(string oemSerialNo);
    }
}