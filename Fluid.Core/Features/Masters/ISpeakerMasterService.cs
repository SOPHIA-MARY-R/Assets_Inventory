using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface ISpeakerMasterService
    {
        Task<Result<string>> AddAsync(SpeakerModel model);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(SpeakerModel model);
        Task<PaginatedResult<SpeakerModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<SpeakerInfo>> GetByIdAsync(string oemSerialNo);
    }
}