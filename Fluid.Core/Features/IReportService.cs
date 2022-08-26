using Fluid.Shared.Models.FilterModels;

namespace Fluid.Core.Features;

public interface IReportService
{
    Task<IResult<string>> ExportToExcelAsync<T>(IEnumerable<T> data,
        Dictionary<string, Func<T, object>> mappings,
        string sheetName = "sheet1");

    Task<IResult<string>> ExportFeedLogsAsync(FeedLogFilter filter);
    Task<IResult<string>> ExportHardwareChangeLogsAsync(HardwareChangeLogFilter filter);
}