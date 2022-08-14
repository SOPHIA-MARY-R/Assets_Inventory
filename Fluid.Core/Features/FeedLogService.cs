using System.Text.Json;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.Core.Features;

public class FeedLogService : IFeedLogService
{
    private readonly IUnitOfWork _unitOfWork;

    public FeedLogService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

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
}