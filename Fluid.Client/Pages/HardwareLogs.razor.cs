using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Models.FilterModels;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class HardwareLogs
{
    private MudTable<FeedLog> _feedLogTable;
    private List<FeedLog> _feedLogs = new();
    //private FeedLog _feedLog;
    private readonly FeedLogFilter _filterModel = new();
    private DateRange _searchDateRange;
    private int _totalItems;
    private FeedLogCountDetails _countDetails;

    protected override async Task OnInitializedAsync()
    {
        _searchDateRange = new DateRange(periodService.FromDate, periodService.ToDate);
        var result = await FeedLogHttpClient.GetCountDetails();
        if (result.Succeeded)
        {
            _countDetails = result.Data;
        }
        else
        {
            foreach (var message in result.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
        await base.OnInitializedAsync();
    }

    private async Task AutoValidateLogs()
    {
        await FeedLogHttpClient.AutoValidateLogs();
        await _feedLogTable.ReloadServerData();
    }

    private async Task<TableData<FeedLog>> OnServerReloadAsync(TableState tableState)
    {
        await LoadDataAsync(tableState.Page, tableState.PageSize);
        return new TableData<FeedLog> { TotalItems = _totalItems, Items = _feedLogs };
    }

    private async Task LoadDataAsync(int pageNumber, int pageSize)
    {
        _filterModel.FromDateTimeTicks = periodService.FromDate.Ticks;
        _filterModel.ToDateTimeTicks = periodService.ToDate.Ticks;
        var result = await FeedLogHttpClient.GetAllAsync(pageNumber, pageSize, _filterModel);
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