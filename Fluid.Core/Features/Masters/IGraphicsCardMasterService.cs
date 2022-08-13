using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IGraphicsCardMasterService
    {
        Task<Result<string>> AddAsync(GraphicsCardModel model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(GraphicsCardModel model);
        Task<PaginatedResult<GraphicsCardModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<GraphicsCardInfo>> GetByIdAsync(string oemSerialNo);
    }
}