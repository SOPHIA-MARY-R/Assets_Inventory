using System.Text.Json;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fluid.Client.Pages;

public partial class ViewHardwareChanges
{
    [Parameter]
    public string Id { get; set; }

    private SystemConfiguration Model { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        var result = await MasterHttpClient.GetByIdAsync(Id);
        if (result.Succeeded)
        {
            var feedLog = result.Data;
            Model = (SystemConfiguration)JsonSerializer.Deserialize(feedLog.JsonRaw, typeof(SystemConfiguration));
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
}