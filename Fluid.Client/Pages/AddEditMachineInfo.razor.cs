using Fluid.Shared.Entities;
using Fluid.Shared.Enums;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class AddEditMachineInfo
{
    [Parameter]
    public string Id { get; set; }
    private SystemConfiguration _model = new SystemConfiguration(); 
    private SystemConfiguration _systemConfiguration = new SystemConfiguration();

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
            _model.Motherboard = result.Data;
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
                _model.Motherboard = new MotherboardInfo();
                break;
            case HardwareSelectionType.Empty:
                _model.Motherboard = null;
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
        await base.OnInitializedAsync();
    }
    
}