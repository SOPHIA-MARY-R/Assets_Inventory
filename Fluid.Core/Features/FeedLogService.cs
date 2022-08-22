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

    public async Task<IResult<FeedLog>> GetById(string id)
    {
        try
        {
            var feedLog = await _unitOfWork.GetRepository<FeedLog>().GetByIdAsync(id);
            if (feedLog is null)
                throw new Exception("The Requested Log record is not found");
            return await Result<FeedLog>.SuccessAsync(feedLog);
        }
        catch (Exception e)
        {
            return await Result<FeedLog>.FailAsync(e.Message);
        }
    }

    public async Task<IResult<SystemConfiguration>> SaveLog(SystemConfiguration systemConfiguration)
    {
        try
        {
            var existingFeedLog = await _unitOfWork.GetRepository<FeedLog>().Entities.FirstOrDefaultAsync(x => x.AssetTag == systemConfiguration.MachineDetails.AssetTag);
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

            var assetTag = systemConfiguration.MachineDetails.AssetTag;
            var machineInfo = await _unitOfWork.GetRepository<MachineInfo>()
                .GetByIdAsync(assetTag);
            SystemConfiguration existingSystemConfiguration;
            var changeConfigAtClient = false;
            if (machineInfo is not null)
            {
                existingSystemConfiguration = (await _systemConfigurationService.GetSystemConfiguration(assetTag)).Data;
                changeConfigAtClient = machineInfo.UpdateChangeOnClient;
                machineInfo.UpdateChangeOnClient = false;
                await _unitOfWork.GetRepository<MachineInfo>().UpdateAsync(machineInfo, assetTag);
            }
            else
            {
                existingSystemConfiguration =
                    (SystemConfiguration)JsonSerializer.Deserialize(existingFeedLog.JsonRaw,
                        typeof(SystemConfiguration));
            }
            if (systemConfiguration == existingSystemConfiguration)
            {
                existingFeedLog.LogDateTime = DateTime.Now;
            }
            else
            {
                var expectedFeedLogSysConfig = changeConfigAtClient ? existingSystemConfiguration : systemConfiguration;
                existingFeedLog.AssetBranch = systemConfiguration.MachineDetails.AssetBranch;
                existingFeedLog.AssetLocation = systemConfiguration.MachineDetails.AssetLocation;
                existingFeedLog.AssignedPersonName = systemConfiguration.MachineDetails.AssignedPersonName;
                existingFeedLog.JsonRaw = JsonSerializer.Serialize(expectedFeedLogSysConfig, typeof(SystemConfiguration));
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
            if (changeConfigAtClient)
            {
                return await Result<SystemConfiguration>.SuccessAsync(existingSystemConfiguration);
            }
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
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<Result<FeedLogCountDetails>> GetCountDetails()
    {
        try
        {
            var countDetails = new FeedLogCountDetails()
            {
                Accepted = await _unitOfWork.GetRepository<FeedLog>().Entities
                    .Specify(new FeedLogAttendStatusSpecification(LogAttendStatus.Accepted)).CountAsync(),
                Ignored = await _unitOfWork.GetRepository<FeedLog>().Entities
                    .Specify(new FeedLogAttendStatusSpecification(LogAttendStatus.Ignored)).CountAsync(),
                NewLogs = await _unitOfWork.GetRepository<FeedLog>().Entities
                    .Specify(new FeedLogAttendStatusSpecification(LogAttendStatus.Unattended)).CountAsync(),
                Pending = await _unitOfWork.GetRepository<FeedLog>().Entities
                    .Specify(new FeedLogAttendStatusSpecification(LogAttendStatus.Pending)).CountAsync(),
                TotalLogs = await _unitOfWork.GetRepository<FeedLog>().CountAsync()
            };
            var existingAssetTags = await _unitOfWork.GetRepository<MachineInfo>().Entities.Select(x => x.AssetTag)
                .ToListAsync();
            countDetails.NewMachines = await _unitOfWork.GetRepository<FeedLog>().Entities.CountAsync(x => existingAssetTags.Contains(x.AssetTag));
            return await Result<FeedLogCountDetails>.SuccessAsync(countDetails);
        }
        catch (Exception e)
        {
            return await Result<FeedLogCountDetails>.FailAsync(e.Message);
        }
    }
}