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
            Expression<Func<CameraInfo, CameraInfo>> expressionMap = info => new CameraInfo
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                MegaPixels = info.MegaPixels,
                Resolution = info.Resolution,
                HasBuiltInMic=info.HasBuiltInMic,
                IsWireLess=info.IsWireLess,
                Model = info.Model,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
            };
            var specification = new CameraInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<CameraInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<CameraInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
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
            return cameraInfo is not null ? Result<CameraInfo>.Success(cameraInfo) : throw new Exception("Camera not found");
        }
        catch (Exception e)
        {
            return Result<CameraInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(CameraInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<CameraInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Camera with OEM Serial Number {model.OemSerialNo} already exists");
            var cameraInfo = new CameraInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MegaPixels = model.MegaPixels,
                Resolution = model.Resolution,
                HasBuiltInMic = model.HasBuiltInMic,
                IsWireLess = model.IsWireLess,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<CameraInfo>().AddAsync(cameraInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added Camera successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(CameraInfo model)
    {
        try
        {
            var oldCameraInfo = await _unitOfWork.GetRepository<CameraInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldCameraInfo is null) throw new Exception("Camera not found");
            var updatedCameraInfo = new CameraInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MegaPixels=model.MegaPixels,
                HasBuiltInMic=model.HasBuiltInMic,
                IsWireLess=model.IsWireLess,
                Resolution = model.Resolution,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<CameraInfo>().UpdateAsync(updatedCameraInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated Camera successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
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
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
