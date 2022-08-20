using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface IMotherboardMasterService
    {
        Task<Result<string>> AddAsync(MotherboardInfo model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(MotherboardInfo model);
        Task<PaginatedResult<MotherboardInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<MotherboardInfo>> GetByIdAsync(string oemSerialNo);
    }
}