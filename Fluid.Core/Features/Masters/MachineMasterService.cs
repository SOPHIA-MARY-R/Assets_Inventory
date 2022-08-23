using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;

namespace Fluid.Core.Features.Masters;

public class MachineMasterService : IMachineMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MachineMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MachineInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new MachineInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MachineInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MachineInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<MachineInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MachineInfo>> GetByIdAsync(string assetTag)
    {
        try
        {
            var machineInfo = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag);
            return machineInfo is not null ? await Result<MachineInfo>.SuccessAsync(machineInfo) : throw new Exception("HardDisk not found");
        }
        catch (Exception e)
        {
            return await Result<MachineInfo>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> AddAsync(MachineInfo machineInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(machineInfo.AssetTag) is not null)
                throw new Exception($"Machine with Asset Tag {machineInfo.AssetTag} already exists");
            await _unitOfWork.GetRepository<MachineInfo>().AddAsync(machineInfo);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Added Machine successfully");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> EditAsync(MachineInfo machineInfo, string assetTag)
    {
        try
        {
            var oldMachineInfo = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(machineInfo.OemSerialNo);
            if (oldMachineInfo is null) throw new Exception("Machine not found");
            await _unitOfWork.GetRepository<MachineInfo>().UpdateAsync(machineInfo, machineInfo.AssetTag);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Updated Machine successfully");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> DeleteAsync(string assetTag)
    {
        try
        {
            var machineInfo = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag);
            if (machineInfo is null) throw new Exception("Machine not found");
            await _unitOfWork.GetRepository<MachineInfo>().DeleteAsync(machineInfo);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
}
