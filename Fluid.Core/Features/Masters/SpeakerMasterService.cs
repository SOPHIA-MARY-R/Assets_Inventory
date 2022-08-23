using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class SpeakerMasterService : ISpeakerMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public SpeakerMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<SpeakerInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new SpeakerInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<SpeakerInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<SpeakerInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<SpeakerInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<SpeakerInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var speakerInfo = await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(oemSerialNo);
            return speakerInfo is not null ? await Result<SpeakerInfo>.SuccessAsync(speakerInfo) : throw new Exception("Speaker not found");
        }
        catch (Exception e)
        {
            return await Result<SpeakerInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(SpeakerInfo speakerInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(speakerInfo.OemSerialNo) is not null)
                throw new Exception($"Speaker with OEM Serial Number {speakerInfo.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<SpeakerInfo>().AddAsync(speakerInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(speakerInfo.OemSerialNo, "Added Speaker successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(SpeakerInfo speakerInfo)
    {
        try
        {
            var oldSpeakerInfo = await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(speakerInfo.OemSerialNo);
            if (oldSpeakerInfo is null) throw new Exception("Speaker not found");
            await _unitOfWork.GetRepository<SpeakerInfo>().UpdateAsync(speakerInfo, speakerInfo.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(speakerInfo.OemSerialNo, "Updated Speaker successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> DeleteAsync(string oemSerialNo)
    {
        try
        {
            var speakerInfo = await _unitOfWork.GetRepository<SpeakerInfo>().GetByIdAsync(oemSerialNo);
            if (speakerInfo is null) throw new Exception("Speaker not found");
            await _unitOfWork.GetRepository<SpeakerInfo>().DeleteAsync(speakerInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
