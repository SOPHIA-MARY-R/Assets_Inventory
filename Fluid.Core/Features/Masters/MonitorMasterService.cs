using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class MonitorMasterService : IMonitorMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MonitorMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MonitorModel>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            Expression<Func<MonitorInfo, MonitorModel>> expressionMap = info => new MonitorModel
            {
                OemSerialNo = info.OemSerialNo,
                Manufacturer = info.Manufacturer,
                PanelType = info.PanelType,
                RefreshRate = info.RefreshRate,
                HasBuiltInSpeakers = info.HasBuiltInSpeakers,
                HDMIPortCount = info.HDMIPortCount,
                VGAPortCount = info.VGAPortCount,
                Model = info.Model,
                Price = info.Price,
                PurchaseDate = info.PurchaseDate,
                MachineId = info.MachineId,
                Description = info.Description,
            };
            var specification = new MonitorInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MonitorInfo>().Entities.Specify(specification).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MonitorInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).Select(expressionMap).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<MonitorModel>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MonitorInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var monitorInfo = await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(oemSerialNo);
            return monitorInfo is not null ? Result<MonitorInfo>.Success(monitorInfo) : throw new Exception("Monitor not found");
        }
        catch (Exception e)
        {
            return Result<MonitorInfo>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(MonitorModel model)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(model.OemSerialNo) is not null)
                throw new Exception($"Monitor with OEM Serial Number {model.OemSerialNo} already exists");
            var monitorInfo = new MonitorInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                PanelType = model.PanelType,
                HasBuiltInSpeakers = model.HasBuiltInSpeakers,
                HDMIPortCount = model.HDMIPortCount,
                VGAPortCount = model.VGAPortCount,
                RefreshRate = model.RefreshRate,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<MonitorInfo>().AddAsync(monitorInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Added Monitor successfully");
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(MonitorModel model)
    {
        try
        {
            var oldMonitorInfo = await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(model.OemSerialNo);
            if (oldMonitorInfo is null) throw new Exception("Monitor not found");
            var updatedMonitorInfo = new MonitorInfo
            {
                OemSerialNo = model.OemSerialNo,
                Manufacturer = model.Manufacturer,
                Model = model.Model,
                PanelType = model.PanelType,
                HasBuiltInSpeakers = model.HasBuiltInSpeakers,
                HDMIPortCount = model.HDMIPortCount,
                VGAPortCount = model.VGAPortCount,
                RefreshRate = model.RefreshRate,
                MachineId = model.MachineId,
                Description = model.Description,
                Price = model.Price,
                PurchaseDate = model.PurchaseDate
            };
            await _unitOfWork.GetRepository<MonitorInfo>().UpdateAsync(updatedMonitorInfo, model.OemSerialNo);
            await _unitOfWork.Commit();
            return Result<string>.Success(model.OemSerialNo, "Updated Monitor successfully");
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
            var monitorInfo = await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(oemSerialNo);
            if (monitorInfo is null) throw new Exception("Monitor not found");
            await _unitOfWork.GetRepository<MonitorInfo>().DeleteAsync(monitorInfo);
            await _unitOfWork.Commit();
            return Result<string>.Success(oemSerialNo);
        }
        catch (Exception e)
        {
            return Result<string>.Fail(e.Message);
        }
    }
}
