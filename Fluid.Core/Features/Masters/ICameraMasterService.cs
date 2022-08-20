using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface ICameraMasterService
    {
        Task<Result<string>> AddAsync(CameraInfo model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(CameraInfo model);
        Task<PaginatedResult<CameraInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<CameraInfo>> GetByIdAsync(string oemSerialNo);
    }
}