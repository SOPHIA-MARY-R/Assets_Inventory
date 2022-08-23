using System.Text.Json;
using EFCore.BulkExtensions;
using Fluid.Core.Extensions;
using Fluid.Core.Interfaces;
using Fluid.Core.Specifications;
using Fluid.Core.Specifications.FeedLogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums;
using Fluid.Shared.Models;
using Fluid.Shared.Models.FilterModels;
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

    public async Task<IResult> AcceptLog(string id)
    {
        try
        {
            var feedLog = await _unitOfWork.GetRepository<FeedLog>().GetByIdAsync(id);
            if (feedLog is null)
                throw new Exception("FeedLog not found");
            var systemConfiguration = JsonSerializer.Deserialize<SystemConfiguration>(feedLog.JsonRaw);
            if (systemConfiguration is null)
                throw new Exception("Unable to parse System Configuration from the hardware log");
            var assetTag = feedLog.AssetTag;
            var oldSystemConfigurationCopy = (await _systemConfigurationService.GetSystemConfiguration(assetTag)).Data; 
            await _unitOfWork.GetRepository<MachineInfo>().UpdateAsync(systemConfiguration.MachineDetails, assetTag);

            var previousMotherboards = await _unitOfWork.GetRepository<MotherboardInfo>().Entities
                .Specify(new MotherboardInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousMotherboard in previousMotherboards)
            {
                previousMotherboard.MachineId = null;
                previousMotherboard.UseStatus = UseStatus.UnderSpare;
            }
            foreach (var motherboard in systemConfiguration.Motherboards)
            {
                var oemSerialNo = motherboard.OemSerialNo;
                motherboard.MachineId = assetTag;
                motherboard.UseStatus = UseStatus.InUse;
                if (await _unitOfWork.GetRepository<MotherboardInfo>().GetByIdAsync(motherboard.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<MotherboardInfo>().AddAsync(motherboard);
                else
                    await _unitOfWork.GetRepository<MotherboardInfo>().UpdateAsync(motherboard, oemSerialNo);
            }

            var previousPhysicalMemories = await _unitOfWork.GetRepository<PhysicalMemoryInfo>().Entities
                .Specify(new PhysicalMemoryInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var physicalMemoryInfo in previousPhysicalMemories)
            {
                physicalMemoryInfo.MachineId = null;
                physicalMemoryInfo.UseStatus = UseStatus.UnderSpare;
            }
            foreach (var physicalMemory in systemConfiguration.PhysicalMemories)
            {
                var oemSerialNo = physicalMemory.OemSerialNo;
                physicalMemory.MachineId = assetTag;
                physicalMemory.UseStatus = UseStatus.InUse;
                if (await _unitOfWork.GetRepository<PhysicalMemoryInfo>().GetByIdAsync(physicalMemory.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<PhysicalMemoryInfo>().AddAsync(physicalMemory);
                else
                    await _unitOfWork.GetRepository<PhysicalMemoryInfo>().UpdateAsync(physicalMemory, oemSerialNo);
            }
            
            var previousHardDisks = await _unitOfWork.GetRepository<HardDiskInfo>().Entities
                .Specify(new HardDiskInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var hardDisk in previousHardDisks)
            {
                hardDisk.MachineId = null;
                hardDisk.UseStatus = UseStatus.UnderSpare;
            }
            foreach (var hardDisk in systemConfiguration.HardDisks)
            {
                var oemSerialNo = hardDisk.OemSerialNo;
                hardDisk.MachineId = assetTag;
                hardDisk.UseStatus = UseStatus.InUse;
                if (await _unitOfWork.GetRepository<HardDiskInfo>().GetByIdAsync(hardDisk.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<HardDiskInfo>().AddAsync(hardDisk);
                else
                    await _unitOfWork.GetRepository<HardDiskInfo>().UpdateAsync(hardDisk, oemSerialNo);
            }
            
            var previousKeyboards = await _unitOfWork.GetRepository<KeyboardInfo>().Entities
                .Specify(new KeyboardInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousKeyboard in previousKeyboards)
            {
                previousKeyboard.MachineId = null;
                previousKeyboard.UseStatus = UseStatus.UnderSpare;
            }
            foreach (var keyboard in systemConfiguration.Keyboards)
            {
                var oemSerialNo = keyboard.OemSerialNo;
                keyboard.MachineId = assetTag;
                keyboard.UseStatus = UseStatus.InUse;
                if (await _unitOfWork.GetRepository<KeyboardInfo>().GetByIdAsync(keyboard.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<KeyboardInfo>().AddAsync(keyboard);
                else
                    await _unitOfWork.GetRepository<KeyboardInfo>().UpdateAsync(keyboard, oemSerialNo);
            }
            
            var previousMonitors = await _unitOfWork.GetRepository<MonitorInfo>().Entities
                .Specify(new MonitorInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousMonitor in previousMonitors)
            {
                previousMonitor.MachineId = null;
                previousMonitor.UseStatus = UseStatus.UnderSpare;
            }
            foreach (var monitor in systemConfiguration.Monitors)
            {
                var oemSerialNo = monitor.OemSerialNo;
                monitor.MachineId = assetTag;
                monitor.UseStatus = UseStatus.InUse;
                if (await _unitOfWork.GetRepository<MonitorInfo>().GetByIdAsync(monitor.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<MonitorInfo>().AddAsync(monitor);
                else
                    await _unitOfWork.GetRepository<MonitorInfo>().UpdateAsync(monitor, oemSerialNo);
            }
            
            var previousMouses = await _unitOfWork.GetRepository<MouseInfo>().Entities
                .Specify(new MouseInfoAssetTagSpecification(assetTag))
                .ToListAsync();
            foreach (var previousMouse in previousMouses)
            {
                previousMouse.MachineId = null;
                previousMouse.UseStatus = UseStatus.UnderSpare;
            }
            foreach (var mouse in systemConfiguration.Mouses)
            {
                var oemSerialNo = mouse.OemSerialNo;
                mouse.MachineId = assetTag;
                mouse.UseStatus = UseStatus.InUse;
                if (await _unitOfWork.GetRepository<MouseInfo>().GetByIdAsync(mouse.OemSerialNo) is null)
                    await _unitOfWork.GetRepository<MouseInfo>().AddAsync(mouse);
                else
                    await _unitOfWork.GetRepository<MouseInfo>().UpdateAsync(mouse, oemSerialNo);
            }
            
            feedLog.LogAttendStatus = LogAttendStatus.Accepted;
            feedLog.AttendingTechnicianId = _currentUserService.UserId;
            await _unitOfWork.GetRepository<FeedLog>().UpdateAsync(feedLog, feedLog.Id);

            var hardwareChangeLog = new HardwareChangeLog
            {
                Id = Guid.NewGuid()
                    .ToString(),
                AssetTag = assetTag,
                OemSerialNo = systemConfiguration.MachineDetails.OemSerialNo,
                Manufacturer = systemConfiguration.MachineDetails.Manufacturer,
                OldMachineName = oldSystemConfigurationCopy.MachineDetails.MachineName,
                OldAssignedPersonName = oldSystemConfigurationCopy.MachineDetails.AssignedPersonName,
                OldAssetLocation = oldSystemConfigurationCopy.MachineDetails.AssetLocation,
                OldAssetBranch = oldSystemConfigurationCopy.MachineDetails.AssetBranch,
                AssetBranch = systemConfiguration.MachineDetails.AssetBranch,
                AssetLocation = systemConfiguration.MachineDetails.AssetLocation,
                AssignedPersonName = systemConfiguration.MachineDetails.AssignedPersonName,
                ChangeDateTime = DateTime.Now,
                MachineName = systemConfiguration.MachineDetails.MachineName,
                MachineType = systemConfiguration.MachineDetails.MachineType,
                OldConfigJsonRaw = JsonSerializer.Serialize(oldSystemConfigurationCopy),
                NewConfigJsonRaw = feedLog.JsonRaw,
                Model = systemConfiguration.MachineDetails.Model
            };
            await _unitOfWork.GetRepository<HardwareChangeLog>().AddAsync(hardwareChangeLog);
            
            await _unitOfWork.Commit();
            return await Result.SuccessAsync("Hardware Accepted successfully!");
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> IgnoreLog(string id)
    {
        try
        {
            var feedLog = await _unitOfWork.GetRepository<FeedLog>().GetByIdAsync(id);
            if (feedLog is null)
                throw new Exception("FeedLog not found");
            feedLog.LogAttendStatus = LogAttendStatus.Ignored;
            feedLog.AttendingTechnicianId = _currentUserService.UserId;
            await _unitOfWork.GetRepository<FeedLog>().UpdateAsync(feedLog, id);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.Rollback();
            return await Result.FailAsync(e.Message);
        }
    }
}