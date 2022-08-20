using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class HardDiskMasterService : IHardDiskMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public HardDiskMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<HardDiskInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<HardDiskInfo, HardDiskInfo>> expressionMap = info => new HardDiskInfo
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                Model = info.Model,
                MediaType = info.MediaType,
                BusType = info.BusType,
                HealthCondition = info.HealthCondition,
                Size = info.Size,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
            };
            var specification = new HardDiskInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<HardDiskInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<HardDiskInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<HardDiskInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<HardDiskInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var harddiskInfo = await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(oemSerialNo);
            return harddiskInfo is not null ? Result<HardDiskInfo>.Success(harddiskInfo) : throw new Exception("HardDisk not found");
        }
        catch (Exception e)
        {
            return Result<HardDiskInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(HardDiskInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"HardDisk with OEM Serial Number {model.OemSerialNo} already exists");
            var harddiskInfo = new HardDiskInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MediaType = model.MediaType,
                BusType = model.BusType,
                HealthCondition = model.HealthCondition,
                Size = model.Size,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<HardDiskInfo>().AddAsync(harddiskInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added HardDisk successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(HardDiskInfo model)
    {
        try
        {
            var oldHardDiskInfo = await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldHardDiskInfo is null) throw new Exception("HardDisk not found");
            var updatedHardDiskInfo = new HardDiskInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MediaType=model.MediaType,
                BusType=model.BusType,
                HealthCondition=model.HealthCondition,
                Size=model.Size,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<HardDiskInfo>().UpdateAsync(updatedHardDiskInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated HardDisk successfully");
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
            var harddiskInfo = await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(oemSerialNo);
            if (harddiskInfo is null) throw new Exception("HardDisk not found");
            await _unitOfWork.GetRepository<HardDiskInfo>().DeleteAsync(harddiskInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
