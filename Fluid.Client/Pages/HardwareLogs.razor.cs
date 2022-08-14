using Fluid.Shared.Entities;
using Fluid.Shared.Requests;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class HardwareLogs
{
    private MudTable<FeedLog> _feedLogTable;
    private List<FeedLog> _feedLogs = new();
    private FeedLog _feedLog;
    private readonly FeedLogFilter _filterModel = new();
    private DateRange _searchDateRange;
    private int _totalItems;

    protected override void OnInitialized()
    {
        _searchDateRange = new DateRange(periodService.FromDate, periodService.ToDate);
        base.OnInitialized();
    }

    private async Task<TableData<FeedLog>> OnServerReloadAsync(TableState tableState)
    {
        await LoadDataAsync(tableState.Page, tableState.PageSize, tableState);
        return new TableData<FeedLog> { TotalItems = _totalItems, Items = _feedLogs };
    }

    private async Task LoadDataAsync(int pageNumber, int pageSize, TableState tableState)
    {
        _filterModel.FromDateTimeTicks = periodService.FromDate.Ticks;
        _filterModel.ToDateTimeTicks = periodService.ToDate.Ticks;
        var result = await feedLogHttpClient.GetAllAsync(pageNumber, pageSize, _filterModel);
        if (result.Succeeded)
        {
            _totalItems = result.TotalCount;
            _feedLogs = result.Data;
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