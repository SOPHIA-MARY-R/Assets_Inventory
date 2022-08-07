using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMotherboardMasterService
    {
        Task<Result<string>> AddAsync(MotherboardModel model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(MotherboardModel model);
        Task<PaginatedResult<MotherboardModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MotherboardInfo>> GetByIdAsync(string oemSerialNo);
    }
}