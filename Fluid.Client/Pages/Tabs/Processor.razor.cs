using Fluid.Client.Pages.Dialogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using MudBlazor;
namespace Fluid.Client.Pages.Tabs;

public partial class Processor
{
    private List<ProcessorInfo> _processors;
    private string _searchString;
    private MudTable<ProcessorInfo> _processorTable;
    private int _totalItems;

    private async Task<TableData<ProcessorInfo>> OnServerReloadAsync(TableState tableState)
    {
        if (!string.IsNullOrWhiteSpace(_searchString))
        {
            tableState.Page = 0;
        }
        await LoadDataAsync(tableState.Page, tableState.PageSize, tableState);
        return new TableData<ProcessorInfo> { TotalItems = _totalItems, Items = _processors };
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
            _processors = response.Data;
        }
        else
        {
            foreach (var message in response.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
    }

    private async void InvokeModal(string processorId)
    {
        var parameters = new DialogParameters();
        if (!string.IsNullOrEmpty(processorId))
        {
            var item = _processors.FirstOrDefault(c => c.ProcessorId == processorId);
            if (item != null)
            {
                parameters.Add(nameof(ProcessorDialog.Model), new ProcessorInfo
                {
                    ProcessorId = item.ProcessorId,
                    Name = item.Name,
                    Manufacturer = item.Manufacturer,
                    Family = item.Family,
                    NumberOfCores = item.NumberOfCores,
                    NumberOfLogicalProcessors = item.NumberOfLogicalProcessors,
                    ThreadCount = item.ThreadCount,
                    MaxClockSpeed = item.MaxClockSpeed,
                    PurchaseDate = item.PurchaseDate,
                    Description = item.Description,
                    Price = item.Price,
                    UseStatus = item.UseStatus,
                });
                parameters.Add(nameof(ProcessorDialog.IsEditMode), true);
            }
        }
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<ProcessorDialog>(string.IsNullOrEmpty(processorId) ? "Add" : "Update", parameters, options);
        if (!(await dialog.Result).Cancelled)
        {
            OnSearch("");
        }
    }

    private async Task Delete(string id)
    {
        if ((await dialogService.ShowMessageBox("Confirm Delete?", "Are you sure want to delete this Processor? This action cannot be undone", yesText: "Delete", cancelText: "Cancel")) == true)
        {
            var response = await masterHttpClient.DeleteAsync(id);
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
        _processorTable.ReloadServerData();
    }
}
