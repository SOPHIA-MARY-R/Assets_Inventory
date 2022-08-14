﻿using System.Text.Json;
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
    private readonly ICurrentUserService _currentUserService;

    public FeedLogService(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<IResult> SaveLog(SystemConfiguration systemConfiguration)
    {
        try
        {
            var feedLogStorage = new FeedLog
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
            await _unitOfWork.GetRepository<FeedLog>().AddAsync(feedLogStorage);
            await _unitOfWork.Commit();
            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
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
                .ToPaginatedListAsync(pageNumber, pageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<FeedLog>.Failure(new List<string>() { e.Message });
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