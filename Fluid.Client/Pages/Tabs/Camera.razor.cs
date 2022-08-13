using Fluid.Client.Pages.Dialogs;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using MudBlazor;
namespace Fluid.Client.Pages.Tabs;


public partial class Camera
{
    private List<CameraModel> _Cameras;
    private string _searchString;
    private MudTable<CameraModel> _CameraTable;
    private int _totalItems;

    private async Task<TableData<CameraModel>> OnServerReloadAsync(TableState tableState)
    {
        if (!string.IsNullOrWhiteSpace(_searchString))
        {
            tableState.Page = 0;
        }
        await LoadDataAsync(tableState.Page, tableState.PageSize, tableState);
        return new TableData<CameraModel> { TotalItems = _totalItems, Items = _Cameras };
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
            _Cameras = response.Data;
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
            var item = _Cameras.FirstOrDefault(c => c.OemSerialNo == oemSerialNo);
            if (item != null)
            {
                parameters.Add(nameof(CameraDialog.Model), new CameraModel
                {
                    OemSerialNo = item.OemSerialNo,
                    Manufacturer = item.Manufacturer,
                    Model = item.Model,
                    MegaPixels = item.MegaPixels,
                    Resolution = item.Resolution,
                    HasBuiltInMic = item.HasBuiltInMic,
                    IsWireLess = item.IsWireLess,
                    PurchaseDate = item.PurchaseDate,
                    Description = item.Description,
                    Price = item.Price
                });
                parameters.Add(nameof(CameraDialog.IsEditMode), true);
            }
        }
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<CameraDialog>(string.IsNullOrEmpty(oemSerialNo) ? "Add" : "Update", parameters, options);
        if (!(await dialog.Result).Cancelled)
        {
            OnSearch("");
        }
    }

    private async Task Delete(string Id)
    {
        if ((await dialogService.ShowMessageBox("Confirm Delete?", "Are you sure want to delete this Camera? This action cannot be undone", yesText: "Delete", cancelText: "Cancel")) == true)
        {
            var response = await masterHttpClient.DeleteAsync(Id);
            OnSearch("");
            foreach (var message in response.Messages)
            {
                if (response.Succeeded)
                {
                    snackbar.Add(message, Severity.Success);
                }
                else
                {
                    snackbar.Add(message, Severity.Error);
                }
            }
        }
    }

    private void OnSearch(string value)
    {
        _searchString = value;
        _CameraTable.ReloadServerData();
    }
}
