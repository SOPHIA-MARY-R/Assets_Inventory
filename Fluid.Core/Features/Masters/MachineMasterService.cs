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
            Expression<Func<MachineInfo, MachineInfo>> expressionMap = info => new MachineInfo
            {
                AssetTag = info.AssetTag,
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                Model = info.Model,
                MachineType = info.MachineType,
                MachineName = info.MachineName,
                UseType = info.UseType,
                UseStatus = info.UseStatus,
                AssetBranch = info.AssetBranch,
                AssetLocation = info.AssetLocation,
                AssignedPersonName = info.AssignedPersonName,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                InitializationDate = info.InitializationDate,
            };
            var specification = new MachineInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MachineInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MachineInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
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

    public async Task<IResult> AddAsync(MachineInfo model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(model.AssetTag) is not null)
                throw new Exception($"Machine with Asset Tag {model.AssetTag} already exists");
            var machineInfo = new MachineInfo
            {
                AssetTag = model.AssetTag,
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineType = model.MachineType,
                MachineName = model.MachineName,
                UseType = model.UseType,
                UseStatus = model.UseStatus,
                AssetBranch = model.AssetBranch,
                AssetLocation = model.AssetLocation,
                AssignedPersonName = model.AssignedPersonName,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate,
                InitializationDate = model.InitializationDate,
            };
            await _unitOfWork.GetRepository<MachineInfo>().AddAsync(machineInfo);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Added Machine successfully");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> EditAsync(MachineInfo model, string assetTag)
    {
        try
        {
            var oldMachineInfo = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldMachineInfo is null) throw new Exception("Machine not found");
            var updatedMachineInfo = new MachineInfo
            {
                AssetTag = model.AssetTag,
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                MachineType = model.MachineType,
                MachineName = model.MachineName,
                UseType = model.UseType,
                UseStatus = model.UseStatus,
                AssetBranch = model.AssetBranch,
                AssetLocation = model.AssetLocation,
                AssignedPersonName = model.AssignedPersonName,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate,
                InitializationDate = model.InitializationDate,
            };
            await _unitOfWork.GetRepository<MachineInfo>().UpdateAsync(updatedMachineInfo, model.AssetTag);
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
