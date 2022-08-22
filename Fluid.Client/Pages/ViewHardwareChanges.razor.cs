using System.Text.Json;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Extensions;

namespace Fluid.Client.Pages;

public partial class ViewHardwareChanges
{
    [Parameter]
    public string Id { get; set; }

    private SystemConfiguration Model { get; set; } = new();

    private SystemConfiguration SysConfigFromMaster = new();

    protected override async Task OnParametersSetAsync()
    {
        var result = await FeedLogHttpClient.GetByIdAsync(Id);
        if (result.Succeeded)
        {
            var feedLog = result.Data;
            Model = (SystemConfiguration)JsonSerializer.Deserialize(feedLog.JsonRaw, typeof(SystemConfiguration));
            var assetTag = Model.MachineDetails.AssetTag;
            var sysConfigResult = await MachineMasterHttpClient.GetByIdAsync(assetTag);
            if (sysConfigResult.Succeeded)
            {
                SysConfigFromMaster = sysConfigResult.Data;
            }
            else
            {
                await AddNewMachineNotifier();
            }
        }
        else
        {
            foreach (var message in result.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
            navigationManager.NavigateTo("/Hardware-Logs");
        }
        await base.OnParametersSetAsync();
    }

    private async Task AddNewMachineNotifier()
    {
        if (await dialogService.ShowMessageBox("New Machine found",
                "Would you like to add this machine into the master? This will redirect you to add machine page.",
                "Yes", "No") == true)
        {
            navigationManager.NavigateTo($"Machine-Master/{Id}/Add");
        }
    }
}