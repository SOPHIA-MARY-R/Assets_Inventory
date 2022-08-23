using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters
{
    public interface ISpeakerMasterService
    {
        Task<Result<string>> AddAsync(SpeakerInfo speakerInfo);
        Task<Result<string>> DeleteAsync(string oemSerialNo);
        Task<Result<string>> EditAsync(SpeakerInfo speakerInfo);
        Task<PaginatedResult<SpeakerInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy);
        Task<Result<SpeakerInfo>> GetByIdAsync(string oemSerialNo);
    }
}