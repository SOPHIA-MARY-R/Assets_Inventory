using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class PhysicalMemoryMasterService : IPhysicalMemoryMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public PhysicalMemoryMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<PhysicalMemoryInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<PhysicalMemoryInfo, PhysicalMemoryInfo>> expressionMap = info => new PhysicalMemoryInfo
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
                UseStatus = info.UseStatus,
                Capacity = info.Capacity,
                Speed = info.Speed,
                MemoryType = info.MemoryType,
                FormFactor = info.FormFactor
            };
            var specification = new PhysicalMemoryInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<PhysicalMemoryInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<PhysicalMemoryInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var physicalmemoryInfo = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(oemSerialNo);
            return physicalmemoryInfo is not null ? Result<PhysicalMemoryInfo>.Success(physicalmemoryInfo) : throw new Exception("PhysicalMemory not found");
        }
        catch (Exception e)
        {
            return Result<PhysicalMemoryInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(PhysicalMemoryInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"PhysicalMemory with OEM Serial Number {model.OemSerialNo} already exists");
            var physicalmemoryInfo = new PhysicalMemoryInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                UseStatus = model.UseStatus,
                Capacity = model.Capacity,
                Speed = model.Speed,
                MemoryType = model.MemoryType,
                FormFactor = model.FormFactor,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<PhysicalMemoryInfo>().AddAsync(physicalmemoryInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added PhysicalMemory successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(PhysicalMemoryInfo model)
    {
        try
        {
            var oldPhysicalMemoryInfo = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldPhysicalMemoryInfo is null) throw new Exception("PhysicalMemory not found");
            var updatedPhysicalMemoryInfo = new PhysicalMemoryInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                UseStatus = model.UseStatus,
                Capacity = model.Capacity,
                Speed = model.Speed,
                MemoryType = model.MemoryType,
                FormFactor = model.FormFactor,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<PhysicalMemoryInfo>().UpdateAsync(updatedPhysicalMemoryInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated PhysicalMemory successfully");
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
            var physicalmemoryInfo = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(oemSerialNo);
            if (physicalmemoryInfo is null) throw new Exception("PhysicalMemory not found");
            await _unitOfWork.GetRepository<PhysicalMemoryInfo>().DeleteAsync(physicalmemoryInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
