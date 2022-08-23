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
            var specification = new PhysicalMemoryInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
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
            var physicalMemoryInfo = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(oemSerialNo);
            return physicalMemoryInfo is not null ? await Result<PhysicalMemoryInfo>.SuccessAsync(physicalMemoryInfo) : throw new Exception("PhysicalMemory not found");
        }
        catch (Exception e)
        {
            return await Result<PhysicalMemoryInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(PhysicalMemoryInfo physicalMemoryInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(physicalMemoryInfo.OemSerialNo) is not null)
                throw new Exception($"PhysicalMemory with OEM Serial Number {physicalMemoryInfo.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<PhysicalMemoryInfo>().AddAsync(physicalMemoryInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(physicalMemoryInfo.OemSerialNo, "Added PhysicalMemory successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(PhysicalMemoryInfo physicalMemoryInfo)
    {
        try
        {
            var oldPhysicalMemoryInfo = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(physicalMemoryInfo.OemSerialNo);
            if (oldPhysicalMemoryInfo is null) throw new Exception("PhysicalMemory not found");
            await _unitOfWork.GetRepository<PhysicalMemoryInfo>().UpdateAsync(physicalMemoryInfo, physicalMemoryInfo.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(physicalMemoryInfo.OemSerialNo, "Updated PhysicalMemory successfully");
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
            var physicalmemoryInfo = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(oemSerialNo);
            if (physicalmemoryInfo is null) throw new Exception("PhysicalMemory not found");
            await _unitOfWork.GetRepository<PhysicalMemoryInfo>().DeleteAsync(physicalmemoryInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
