using Fluid.Client.Pages.Dialogs.MachineMasterDialogs;
using Fluid.Shared.Entities;
using Fluid.Shared.Enums;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class AddEditMachine
{
    [Parameter]
    public string Id { get; set; }
    private SystemConfiguration _model = new(); 

    private string _motherboardOemSerialNo;
    private HardwareSelectionType _motherboardSelectionType = HardwareSelectionType.Existing;

    private async Task LoadMotherboard()
    {
        if (string.IsNullOrEmpty(_motherboardOemSerialNo))
        {
            snackbar.Add("Please enter a valid Motherboard Serial No");
            return;
        }
        var result = await MotherboardMasterHttpClient.GetByIdAsync(_motherboardOemSerialNo);
        if (result.Succeeded)
        {
            _model.Motherboards = new List<MotherboardInfo>() { result.Data };
            snackbar.Add("Motherboard found successfully");
        }
        else
        {
            foreach (var message in result.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
    }

    private async Task OnMotherboardSelectionType(HardwareSelectionType selectionType)
    {
        switch (selectionType)
        {
            case HardwareSelectionType.Existing:
                await LoadMotherboard();
                break;
            case HardwareSelectionType.New:
                _model.Motherboards = new List<MotherboardInfo>();
                break;
            case HardwareSelectionType.Empty:
                _model.Motherboards = null;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(selectionType), selectionType, null);
        }
        _motherboardSelectionType = selectionType;
    }
    
    protected async override Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            var result = await MasterHttpClient.GetByIdAsync(Id);
            if (result.Succeeded)
            {
                _model = result.Data;
            }
            else
            {
                foreach (var message in result.Messages)
                {
                    snackbar.Add(message, Severity.Error);
                }
            }
        }

        _model.Motherboards = new List<MotherboardInfo>();
        await base.OnInitializedAsync();
    }

    private async Task InvokeMotherboardDialog(bool isNew, bool isEdit, MotherboardInfo info)
    {
        var parameters = new DialogParameters
        {
            { nameof(AddEditMachineMotherboardDialog.IsNew), isNew },
            { nameof(AddEditMachineMotherboardDialog.IsEdit), isEdit },
            { nameof(AddEditMachineMotherboardDialog.Model), info }
        };
        var options = new DialogOptions { CloseButton = true, FullWidth = true, DisableBackdropClick = true, Position = DialogPosition.TopCenter };
        var dialog = dialogService.Show<AddEditMachineMotherboardDialog>("", parameters, options);
        var result = await dialog.Result;
        if (result.Cancelled) return;
        var updatedInfo = result.Data as MotherboardInfo;
        var oemSerialNo = updatedInfo?.OemSerialNo.Trim();
        if (_model.Motherboards.Any(x => x.OemSerialNo.Trim() == oemSerialNo))
            _model.Motherboards.Remove(_model.Motherboards.First(x => x.OemSerialNo.Trim() == oemSerialNo));
        _model.Motherboards.Add(updatedInfo);
    }

    private void DeleteMotherboardInfo(MotherboardInfo motherboardInfo)
    {
        _model.Motherboards.Remove(motherboardInfo);
    }
}