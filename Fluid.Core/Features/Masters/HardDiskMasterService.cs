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
            var specification = new HardDiskInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<HardDiskInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<HardDiskInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
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
            return harddiskInfo is not null ? await Result<HardDiskInfo>.SuccessAsync(harddiskInfo) : throw new Exception("HardDisk not found");
        }
        catch (Exception e)
        {
            return await Result<HardDiskInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(HardDiskInfo hardDiskInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(hardDiskInfo.OemSerialNo) is not null)
                throw new Exception($"HardDisk with OEM Serial Number {hardDiskInfo.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<HardDiskInfo>().AddAsync(hardDiskInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(hardDiskInfo.OemSerialNo, "Added HardDisk successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(HardDiskInfo hardDiskInfo)
    {
        try
        {
            var oldHardDiskInfo = await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(hardDiskInfo.OemSerialNo);
            if (oldHardDiskInfo is null) throw new Exception("HardDisk not found");
            await _unitOfWork.GetRepository<HardDiskInfo>().UpdateAsync(hardDiskInfo, hardDiskInfo.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(hardDiskInfo.OemSerialNo, "Updated HardDisk successfully");
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
            var hardDiskInfo = await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(oemSerialNo);
            if (hardDiskInfo is null) throw new Exception("HardDisk not found");
            await _unitOfWork.GetRepository<HardDiskInfo>().DeleteAsync(hardDiskInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
