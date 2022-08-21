using System.Text.Json;
using EFCore.BulkExtensions;
using Fluid.Core.Extensions;
using Fluid.Core.Interfaces;
using Fluid.Core.Specifications.FeedLogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;

namespace Fluid.Core.Features;

public class FeedLogService : IFeedLogService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISystemConfigurationService _systemConfigurationService;
    private readonly ICurrentUserService _currentUserService;

    public FeedLogService(IUnitOfWork unitOfWork, ISystemConfigurationService systemConfigurationService, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _systemConfigurationService = systemConfigurationService;
        _currentUserService = currentUserService;
    }

    public async Task<IResult<SystemConfiguration>> SaveLog(SystemConfiguration systemConfiguration)
    {
        try
        {
            var existingFeedLog = await _unitOfWork.GetRepository<FeedLog>()
                .GetByIdAsync(systemConfiguration.MachineDetails.AssetTag);
            if (existingFeedLog is null)
            {
                var feedLog = new FeedLog
                {
                    AssetTag = systemConfiguration.MachineDetails.AssetTag,
                    AssetBranch = systemConfiguration.MachineDetails.AssetBranch,
                    AssetLocation = systemConfiguration.MachineDetails.AssetLocation,
                    AssignedPersonName = systemConfiguration.MachineDetails.AssignedPersonName,
                    Id = Guid.NewGuid()
                        .ToString(),
                    JsonRaw = JsonSerializer.Serialize(systemConfiguration,
                        typeof(SystemConfiguration)),
                    LogDateTime = DateTime.Now,
                    LogAttendStatus = LogAttendStatus.Unattended,
                    MachineName = systemConfiguration.MachineDetails.MachineName,
                    MachineType = systemConfiguration.MachineDetails.MachineType,
                    Manufacturer = systemConfiguration.MachineDetails.Manufacturer,
                    Model = systemConfiguration.MachineDetails.Model,
                    OemSerialNo = systemConfiguration.MachineDetails.OemSerialNo
                };
                await _unitOfWork.GetRepository<FeedLog>().AddAsync(feedLog);
                await _unitOfWork.Commit();
                return await Result<SystemConfiguration>.SuccessAsync(systemConfiguration);
            }

            var existingSysConfig =
                (SystemConfiguration)JsonSerializer.Deserialize(existingFeedLog.JsonRaw, typeof(SystemConfiguration));
            if (systemConfiguration == existingSysConfig)
            {
                existingFeedLog.LogDateTime = DateTime.Now;
            }
            else
            {
                existingFeedLog.AssetBranch = systemConfiguration.MachineDetails.AssetBranch;
                existingFeedLog.AssetLocation = systemConfiguration.MachineDetails.AssetLocation;
                existingFeedLog.AssignedPersonName = systemConfiguration.MachineDetails.AssignedPersonName;
                existingFeedLog.JsonRaw = JsonSerializer.Serialize(systemConfiguration, typeof(SystemConfiguration));
                existingFeedLog.LogDateTime = DateTime.Now;
                existingFeedLog.LogAttendStatus = LogAttendStatus.Unattended;
                existingFeedLog.MachineName = systemConfiguration.MachineDetails.MachineName;
                existingFeedLog.MachineType = systemConfiguration.MachineDetails.MachineType;
                existingFeedLog.Manufacturer = systemConfiguration.MachineDetails.Manufacturer;
                existingFeedLog.Model = systemConfiguration.MachineDetails.Model;
                existingFeedLog.OemSerialNo = systemConfiguration.MachineDetails.OemSerialNo;
            }
            await _unitOfWork.GetRepository<FeedLog>().UpdateAsync(existingFeedLog, existingFeedLog.Id);
            await _unitOfWork.Commit();
            return await Result<SystemConfiguration>.SuccessAsync(systemConfiguration);
        }
        catch (Exception e)
        {
            return await Result<SystemConfiguration>.FailAsync(e.Message);
        }
    }

    public async Task<PaginatedResult<FeedLog>> GetAllAsync(int pageNumber, int pageSize, FeedLogFilter filter)
    {
        try
        {
            return await _unitOfWork.GetRepository<FeedLog>().Entities
                .Specify(new FeedLogAssetBranchSpecification(filter.AssetBranch))
                .Specify(new FeedLogAssetLocationSpecification(filter.AssetLocation))
                .Specify(new FeedLogAssetTagSpecification(filter.AssetTag))
                .Specify(new FeedLogAttendStatusSpecification(filter.LogAttendStatus))
                .Specify(new FeedLogDateRangeSpecification(new DateTime(filter.FromDateTimeTicks), new DateTime(filter.ToDateTimeTicks)))
                .Specify(new FeedLogMachineNameSpecification(filter.MachineName))
                .Specify(new FeedLogMachineTypeSpecification(filter.MachineType))
                .Specify(new FeedLogAssignedPersonNameSpecification(filter.AssignedPersonName))
                .OrderByDescending(x => x.LogDateTime)
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<FeedLog>.Failure(new List<string>() { e.Message });
        }
    }

    public async Task<IResult> AutoValidateLogsAsync()
    {
        try
        {
            var assetTags = await _unitOfWork.GetRepository<FeedLog>().Entities
                .Specify(new FeedLogAttendStatusSpecification(LogAttendStatus.Unattended))
                .Select(x => x.AssetTag)
                .Distinct()
                .ToListAsync();
            foreach (var assetTag in assetTags)
            {
                var feedLogs = await _unitOfWork.GetRepository<FeedLog>().Entities
                    .Specify(new FeedLogAssetTagSpecification(assetTag))
                    .ToListAsync();
                var systemConfiguration = (await _systemConfigurationService.GetSystemConfiguration(assetTag)).Data;
                foreach (var feedLog in feedLogs)
                {
                    var logSysConfig = JsonSerializer.Deserialize(feedLog.JsonRaw, typeof(SystemConfiguration));
                    feedLog.LogAttendStatus = (SystemConfiguration)logSysConfig == systemConfiguration && systemConfiguration != null
                        ? LogAttendStatus.AutoValidated
                        : LogAttendStatus.Pending;
                }
                await _unitOfWork.AppDbContext.BulkUpdateAsync(feedLogs);
            }
            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }
    public async Task<IResult> AttendLog(FeedLog feedLog)
    {
        try
        {
            feedLog.AttendingTechnicianId = _currentUserService.UserId;
            await _unitOfWork.GetRepository<FeedLog>().UpdateAsync(feedLog, feedLog.Id);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync();
        }
    }
}