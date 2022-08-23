using Fluid.Core.Extensions;
using Fluid.Core.Specifications.HardwareChangeLogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Models.FilterModels;

namespace Fluid.Core.Features;

public class HardwareChangeLogService : IHardwareChangeLogService
{
    private readonly IUnitOfWork _unitOfWork;

    public HardwareChangeLogService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<HardwareChangeLog>> GetHardwareChangeLogsAsync(HardwareChangeLogFilter filter)
    {
        try
        {
            if (filter.OrderBy?.Any() != true)
            {
                return await _unitOfWork.GetRepository<HardwareChangeLog>().Entities
                    .Specify(new HardwareChangeLogManufacturerSpecification(filter.Manufacturer))
                    .Specify(new HardwareChangeLogModelSpecification(filter.Model))
                    .Specify(new HardwareChangeLogAssetBranchSpecification(filter.AssetBranch))
                    .Specify(new HardwareChangeLogAssetLocationSpecification(filter.AssetLocation))
                    .Specify(new HardwareChangeLogAssetTagSpecification(filter.AssetTag))
                    .Specify(new HardwareChangeLogAssignedPersonSpecification(filter.AssignedPersonName))
                    .Specify(new HardwareChangeLogMachineNameSpecification(filter.MachineName))
                    .Specify(new HardwareChangeLogMachineTypeSpecification(filter.MachineType))
                    .Where(x => x.ChangeDateTime >= filter.StartDate && x.ChangeDateTime <= filter.EndDate)
                    .ToPaginatedListAsync(filter.PageNumber, filter.PageSize);
            }
            return await _unitOfWork.GetRepository<HardwareChangeLog>().Entities
                .Specify(new HardwareChangeLogManufacturerSpecification(filter.Manufacturer))
                .Specify(new HardwareChangeLogModelSpecification(filter.Model))
                .Specify(new HardwareChangeLogAssetBranchSpecification(filter.AssetBranch))
                .Specify(new HardwareChangeLogAssetLocationSpecification(filter.AssetLocation))
                .Specify(new HardwareChangeLogAssetTagSpecification(filter.AssetTag))
                .Specify(new HardwareChangeLogAssignedPersonSpecification(filter.AssignedPersonName))
                .Specify(new HardwareChangeLogMachineNameSpecification(filter.MachineName))
                .Specify(new HardwareChangeLogMachineTypeSpecification(filter.MachineType))
                .Where(x => x.ChangeDateTime >= filter.StartDate && x.ChangeDateTime <= filter.EndDate)
                .OrderBy(string.Join(",", filter.OrderBy))
                .ToPaginatedListAsync(filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return PaginatedResult<HardwareChangeLog>.Failure(new List<string>() { e.Message });
        }
    }
}