using Fluid.Shared.Entities;
using Fluid.Shared.Models.FilterModels;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class HardwareChangeLogs
{
    private MudTable<HardwareChangeLog> _mudTable;
    private List<HardwareChangeLog> _hardwareChangeLogs = new();
    private readonly HardwareChangeLogFilter _filterModel = new();
    private DateRange _searchDateRange;
    private int _totalItems;

    protected override void OnInitialized()
    {
        _searchDateRange = new DateRange(periodService.FromDate, periodService.ToDate);
        base.OnInitialized();
    }

    private async Task<TableData<HardwareChangeLog>> OnServerReloadAsync(TableState tableState)
    {
        await LoadDataAsync(tableState.Page, tableState.PageSize);
        return new TableData<HardwareChangeLog> { TotalItems = _totalItems, Items = _hardwareChangeLogs };
    }

    private async Task LoadDataAsync(int pageNumber, int pageSize)
    {
        _filterModel.StartDate = periodService.FromDate;
        _filterModel.EndDate = periodService.ToDate;
        var result = await HardwareChangeLogsHttpClient.GetAllAsync(_filterModel);
        if (result.Succeeded)
        {
            _totalItems = result.TotalCount;
            _hardwareChangeLogs = result.Data;
        }
        else
        {
            foreach (var message in result.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
    }

    private void SubmitDateRange(MudPicker<DateTime?> picker)
    {
        picker.Close();
        if (_searchDateRange.Start.HasValue && _searchDateRange.End.HasValue)
        {
            periodService.FromDate = _searchDateRange.Start.Value;
            periodService.ToDate = _searchDateRange.End.Value;
        }
        else
        {
            snackbar.Add("Please select proper time period", Severity.Info);
        }
    }
}