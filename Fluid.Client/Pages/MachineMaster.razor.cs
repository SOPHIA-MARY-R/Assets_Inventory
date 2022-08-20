using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class MachineMaster
{
    private List<MachineInfo> _machines;
    private string _searchString;
    private MudTable<MachineInfo> _machineTable;
    private int _totalItems;

    private async Task<TableData<MachineInfo>> OnServerReloadAsync(TableState tableState)
    {
        if (!string.IsNullOrWhiteSpace(_searchString))
        {
            tableState.Page = 0;
        }
        await LoadDataAsync(tableState.Page, tableState.PageSize, tableState);
        return new TableData<MachineInfo> { TotalItems = _totalItems, Items = _machines };
    }

    private async Task LoadDataAsync(int page, int pageSize, TableState tableState)
    {
        string[] orderings = null;
        if (!string.IsNullOrEmpty(tableState.SortLabel))
        {
            orderings = tableState.SortDirection != SortDirection.None ? new[] { $"{tableState.SortLabel} {tableState.SortDirection}" } : new[] { $"{tableState.SortLabel}" };
        }
        var response = await MasterHttpClient.GetAllAsync(new PagedRequest
        {
            PageNumber = page + 1,
            PageSize = pageSize,
            SearchString = _searchString,
            OrderBy = orderings
        });
        if (response.Succeeded)
        {
            _totalItems = response.TotalCount;
            _machines = response.Data;
        }
        else
        {
            foreach (var message in response.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
    }

    private async Task Delete(string Id)
    {
        if ((await dialogService.ShowMessageBox("Confirm Delete?", "Are you sure want to delete this Machine? This action cannot be undone", yesText: "Delete", cancelText: "Cancel")) == true)
        {
            var response = await MasterHttpClient.DeleteAsync(Id);
            OnSearch("");
            foreach (var message in response.Messages)
            {
                snackbar.Add(message, response.Succeeded ? Severity.Success : Severity.Error);
            }
        }
    }

    private void OnSearch(string value)
    {
        _searchString = value;
        _machineTable.ReloadServerData();
    }
}
