using Fluid.Shared.Enums;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class AddEditMachineInfo
{
    private HardwareSelectionType motherboardSelectionType = HardwareSelectionType.Existing;
    [Parameter]
    public string Id { get; set; }
    private SystemConfiguration Model = new SystemConfiguration(); 
    private SystemConfiguration _systemConfiguration = new SystemConfiguration();
    protected async override Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            var result = await MasterHttpClient.GetByIdAsync(Id);
            if (result.Succeeded)
            {
                Model = result.Data;
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