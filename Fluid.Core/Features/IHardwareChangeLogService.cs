using Fluid.Shared.Entities;
using Fluid.Shared.Models.FilterModels;

namespace Fluid.Core.Features;

public interface IHardwareChangeLogService
{
    Task<PaginatedResult<HardwareChangeLog>> GetHardwareChangeLogsAsync(HardwareChangeLogFilter filter);
}