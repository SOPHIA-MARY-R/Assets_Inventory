using System.Drawing;
using System.Runtime.InteropServices;
using Fluid.Core.Extensions;
using Fluid.Core.Specifications.FeedLogs;
using Fluid.Core.Specifications.HardwareChangeLogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Models.FilterModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Fluid.Core.Features;

public class ReportService : IReportService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<string>> ExportToExcelAsync<T>(IEnumerable<T> data,
        Dictionary<string, Func<T, object>> mappings,
        string sheetName = "sheet1")
    {
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var p = new ExcelPackage();
            p.Workbook.Properties.Author = "Quark";
            p.Workbook.Worksheets.Add("Audit Trails");

            var ws = p.Workbook.Worksheets[0];
            ws.Name = sheetName;
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            var colIndex = 1;
            var rowIndex = 1;

            var headers = mappings.Keys.Select(x => x).ToList();

            foreach (var header in headers)
            {
                var cell = ws.Cells[rowIndex, colIndex];

                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightBlue);

                var border = cell.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                        border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                cell.Value = header;

                colIndex++;
            }

            var dataList = data.ToList();
            foreach (var item in dataList)
            {
                colIndex = 1;
                rowIndex++;

                var result = headers.Select(header => mappings[header](item));

                foreach (var value in result)
                {
                    ws.Cells[rowIndex, colIndex++].Value = value;
                }
            }

            using (ExcelRange autoFilterCells = ws.Cells[1, 1, dataList.Count + 1, headers.Count])
            {
                autoFilterCells.AutoFilter = true;
                autoFilterCells.AutoFitColumns();
            }

            var byteArray = await p.GetAsByteArrayAsync();
            return await Result<string>.SuccessAsync(data: Convert.ToBase64String(byteArray));
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<IResult<string>> ExportFeedLogsAsync(FeedLogFilter filter)
    {
        try
        {
            var hardwareLogs = await _unitOfWork.GetRepository<FeedLog>().Entities
                .Specify(new FeedLogAssetBranchSpecification(filter.AssetBranch))
                .Specify(new FeedLogAssetLocationSpecification(filter.AssetLocation))
                .Specify(new FeedLogAssetTagSpecification(filter.AssetTag))
                .Specify(new FeedLogAttendStatusSpecification(filter.LogAttendStatus))
                .Specify(new FeedLogDateRangeSpecification(new DateTime(filter.FromDateTimeTicks),
                    new DateTime(filter.ToDateTimeTicks)))
                .Specify(new FeedLogMachineNameSpecification(filter.MachineName))
                .Specify(new FeedLogMachineTypeSpecification(filter.MachineType))
                .Specify(new FeedLogAssignedPersonNameSpecification(filter.AssignedPersonName))
                .OrderByDescending(x => x.LogDateTime)
                .ToListAsync();
            return await ExportToExcelAsync(hardwareLogs, new Dictionary<string, Func<FeedLog, object>>()
            {
                { "Asset Tag", x => x.AssetTag },
                { "Log DateTime", x => x.LogDateTime },
                { "Manufacturer", x => x.Manufacturer },
                { "Model", x => x.Model },
                { "Machine Name", x => x.MachineName },
                { "Log Attend Status", x => x.LogAttendStatus.ToString() },
                { "Machine Type", x => x.MachineType },
                { "Assigned PersonName", x => x.AssignedPersonName },
                { "Asset Branch", x => x.AssetBranch },
                { "Asset Location", x => x.AssetLocation },
            }, $"Logs-{DateTime.Now:dd-MM-yyyy hh:mm:ss t}");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }

    public async Task<IResult<string>> ExportHardwareChangeLogsAsync(HardwareChangeLogFilter filter)
    {
        try
        {
            filter.PageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
            List<HardwareChangeLog> hardwareChangeLogs;
            if (filter.OrderBy?.Any() != true)
            {
                hardwareChangeLogs = await _unitOfWork.GetRepository<HardwareChangeLog>().Entities
                    .Specify(new HardwareChangeLogManufacturerSpecification(filter.Manufacturer))
                    .Specify(new HardwareChangeLogModelSpecification(filter.Model))
                    .Specify(new HardwareChangeLogAssetBranchSpecification(filter.AssetBranch))
                    .Specify(new HardwareChangeLogAssetLocationSpecification(filter.AssetLocation))
                    .Specify(new HardwareChangeLogAssetTagSpecification(filter.AssetTag))
                    .Specify(new HardwareChangeLogAssignedPersonSpecification(filter.AssignedPersonName))
                    .Specify(new HardwareChangeLogMachineNameSpecification(filter.MachineName))
                    .Specify(new HardwareChangeLogMachineTypeSpecification(filter.MachineType))
                    .Where(x => x.ChangeDateTime >= filter.StartDate && x.ChangeDateTime <= filter.EndDate)
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();
            }
            else
            {
                hardwareChangeLogs = await _unitOfWork.GetRepository<HardwareChangeLog>().Entities
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
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync();
            }

            return await ExportToExcelAsync(hardwareChangeLogs,
                new Dictionary<string, Func<HardwareChangeLog, object>>()
                {
                    { "Asset Tag", x => x.AssetTag },
                    { "Machine Type", x => x.MachineType },
                    { "Manufacturer", x => x.Manufacturer },
                    { "Model", x => x.Model },
                    { "Machine Name", x => x.MachineName },
                    { "Old Machine Name", x => x.OldMachineName },
                    { "Assigned Person Name", x => x.AssignedPersonName },
                    { "Old Assigned Person Name", x => x.OldAssignedPersonName },
                    { "Asset Location", x => x.AssetLocation },
                    { "Old Asset Location", x => x.OldAssetLocation },
                    { "Asset Branch", x => x.AssetBranch },
                    { "Old Asset Branch", x => x.OldAssetBranch },
                    { "", x => x.ChangeDateTime.ToString("dd-MM-yyyy hh:mm:ss tt") },
                }, $"HardwareLogs-{filter.StartDate:dd-MM-yyyy}-{filter.EndDate:dd-MM-yyyy}");
        }
        catch (Exception e)
        {
            return await Result<string>.FailAsync(e.Message);
        }
    }
}