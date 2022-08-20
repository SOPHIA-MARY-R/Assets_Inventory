using Fluid.Client.Pages.Dialogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using MudBlazor;
namespace Fluid.Client.Pages.Tabs;


public partial class HardDisk
{
    private List<HardDiskInfo> _harddisks;
    private string _searchString;
    private MudTable<HardDiskInfo> _harddiskTable;
    private int _totalItems;

    private async Task<TableData<HardDiskInfo>> OnServerReloadAsync(TableState tableState)
    {
        if (!string.IsNullOrWhiteSpace(_searchString))
        {
            tableState.Page = 0;
        }
        await LoadDataAsync(tableState.Page, tableState.PageSize, tableState);
        return new TableData<HardDiskInfo> { TotalItems = _totalItems, Items = _harddisks };
    }

    private async Task LoadDataAsync(int page, int pageSize, TableState tableState)
    {
        string[] orderings = null;
        if (!string.IsNullOrEmpty(tableState.SortLabel))
        {
            orderings = tableState.SortDirection != SortDirection.None ? new[] { $"{tableState.SortLabel} {tableState.SortDirection}" } : new[] { $"{tableState.SortLabel}" };
        }
        var response = await masterHttpClient.GetAllAsync(new PagedRequest
        {
            PageNumber = page + 1,
            PageSize = pageSize,
            SearchString = _searchString,
            OrderBy = orderings
        });
        if (response.Succeeded)
        {
            _totalItems = response.TotalCount;
            _harddisks = response.Data;
        }
        else
        {
            foreach (var message in response.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
    }

    private async void InvokeModal(string oemSerialNo)
    {
        var parameters = new DialogParameters();
        if (!string.IsNullOrEmpty(oemSerialNo))
        {
            var item = _harddisks.FirstOrDefault(c => c.OemSerialNo == oemSerialNo);
            if (item != null)
            {
                parameters.Add(nameof(HardDiskDialog.Model), new HardDiskInfo
                {
                    OemSerialNo = item.OemSerialNo,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    MediaType = item.MediaType,
                    BusType = item.BusType,
                    HealthCondition = item.HealthCondition,
                    Size = item.Size,
                    PurchaseDate = item.PurchaseDate,
                    Description = item.Description,
                    Price = item.Price
                });
                parameters.Add(nameof(HardDiskDialog.IsEditMode), true);
            }
        }
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<HardDiskDialog>(string.IsNullOrEmpty(oemSerialNo) ? "Add" : "Update", parameters, options);
        if (!(await dialog.Result).Cancelled)
        {
            OnSearch("");
        }
    }

    private async Task Delete(string Id)
    {
        if ((await dialogService.ShowMessageBox("Confirm Delete?", "Are you sure want to delete this HardDisk? This action cannot be undone", yesText: "Delete", cancelText: "Cancel")) == true)
        {
            var response = await masterHttpClient.DeleteAsync(Id);
            OnSearch("");
            if (response.Succeeded)
            {
                snackbar.Add("Deleted Successfully", Severity.Info);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    snackbar.Add(message, Severity.Error);
                }
            }
        }
    }

    private void OnSearch(string value)
    {
        _searchString = value;
        _harddiskTable.ReloadServerData();
    }
}
