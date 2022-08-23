using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class CameraMasterService : ICameraMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public CameraMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<CameraInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new CameraInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<CameraInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<CameraInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<CameraInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<CameraInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var cameraInfo = await _unitOfWork.GetRepository<CameraInfo>().GetByIdAsync(oemSerialNo);
            return cameraInfo is not null ? await Result<CameraInfo>.SuccessAsync(cameraInfo) : throw new Exception("Camera not found");
        }
        catch (Exception e)
        {
            return await Result<CameraInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(CameraInfo cameraInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<CameraInfo>().GetByIdAsync(cameraInfo.OemSerialNo) is not null)
                throw new Exception($"Camera with OEM Serial Number {cameraInfo.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<CameraInfo>().AddAsync(cameraInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(cameraInfo.OemSerialNo, "Added Camera successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(CameraInfo cameraInfo)
    {
        try
        {
            var oldCameraInfo = await _unitOfWork.GetRepository<CameraInfo>().GetByIdAsync(cameraInfo.OemSerialNo);
            if (oldCameraInfo is null) throw new Exception("Camera not found");
            await _unitOfWork.GetRepository<CameraInfo>().UpdateAsync(cameraInfo, cameraInfo.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(cameraInfo.OemSerialNo, "Updated Camera successfully");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> DeleteAsync(string oemSerialNo)
    {
        try
        {
            var cameraInfo = await _unitOfWork.GetRepository<CameraInfo>().GetByIdAsync(oemSerialNo);
            if (cameraInfo is null) throw new Exception("Camera not found");
            await _unitOfWork.GetRepository<CameraInfo>().DeleteAsync(cameraInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
