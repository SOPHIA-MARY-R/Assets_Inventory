using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class MachineMasterService : IMachineMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MachineMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MachineMasterModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<MachineInfo, MachineMasterModel>> expressionMap = info => new MachineMasterModel
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
            return PaginatedResult<MachineMasterModel>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MachineInfo>> GetByIdAsync(string assetTag)
    {
        try
        {
            var machineInfo = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag);
            return machineInfo is not null ? Result<MachineInfo>.Success(machineInfo) : throw new Exception("HardDisk not found");
        }
        catch (Exception e)
        {
            return Result<MachineInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(MachineMasterModel model)
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
            return Result<string>.Success(model.AssetTag, "Added Machine successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(MachineMasterModel model)
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
            return Result<string>.Success(model.AssetTag, "Updated Machine successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> DeleteAsync(string assetTag)
    {
        try
        {
            var machineInfo = await _unitOfWork.GetRepository<MachineInfo>().GetByIdAsync(assetTag);
            if (machineInfo is null) throw new Exception("Machine not found");
            await _unitOfWork.GetRepository<MachineInfo>().DeleteAsync(machineInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(assetTag);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
