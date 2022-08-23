using Fluid.Core.Extensions;
using Fluid.Core.Specifications;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features.Masters;

public class MonitorMasterService : IMonitorMasterService
{
    private readonly IUnitOfWork _unitOfWork;

    public MonitorMasterService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<PaginatedResult<MonitorInfo>> GetAllAsync(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        try
        {
            var specification = new MonitorInfoSearchSpecification(searchString);
            if (orderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<MonitorInfo>().Entities.Specify(specification).ToPaginatedListAsync(pageNumber, pageSize);
            }
            return await _unitOfWork.GetRepository<MonitorInfo>().Entities.Specify(specification).OrderBy(string.Join(",", orderBy)).ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<MonitorInfo>.Failure(new List<string> { e.Message });
        }
    }

    public async Task<Result<MonitorInfo>> GetByIdAsync(string oemSerialNo)
    {
        try
        {
            var monitorInfo = await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(oemSerialNo);
            return monitorInfo is not null ? await Result<MonitorInfo>.SuccessAsync(monitorInfo) : throw new Exception("Monitor not found");
        }
        catch (Exception e)
        {
            return await Result<MonitorInfo>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> AddAsync(MonitorInfo monitorInfo)
    {
        try
        {
            if (await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(monitorInfo.OemSerialNo) is not null)
                throw new Exception($"Monitor with OEM Serial Number {monitorInfo.OemSerialNo} already exists");
            await _unitOfWork.GetRepository<MonitorInfo>().AddAsync(monitorInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(monitorInfo.OemSerialNo, "Added Monitor successfully");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<Result<string>> EditAsync(MonitorInfo monitorInfo)
    {
        try
        {
            var oldMonitorInfo = await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(monitorInfo.OemSerialNo);
            if (oldMonitorInfo is null) throw new Exception("Monitor not found");
            await _unitOfWork.GetRepository<MonitorInfo>().UpdateAsync(monitorInfo, monitorInfo.OemSerialNo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(monitorInfo.OemSerialNo, "Updated Monitor successfully");
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
            var monitorInfo = await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(oemSerialNo);
            if (monitorInfo is null) throw new Exception("Monitor not found");
            await _unitOfWork.GetRepository<MonitorInfo>().DeleteAsync(monitorInfo);
            await _unitOfWork.Commit();
            return await Result<string>.SuccessAsync(oemSerialNo);
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}
