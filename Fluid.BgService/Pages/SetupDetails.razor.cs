using Fluid.BgService.Services;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;

namespace Fluid.BgService.Pages;

public partial class SetupDetails
{
    private SystemConfiguration Model { get; set; } = new();

    protected override Task OnInitializedAsync()
    {
        Model = systemConfigurationService.SystemConfiguration;
        Model.MachineDetails.AssetTag = machineIdentifierService.MachineIdentifier.AssetTag;
        Model.Motherboards = new List<MotherboardInfo> { new MotherboardInfo() { MachineId = Model.MachineDetails.AssetTag } };
        Model.Mouses = new List<MouseInfo> { new MouseInfo() { MachineId = Model.MachineDetails.AssetTag } };
        Model.Keyboards = new List<KeyboardInfo> { new KeyboardInfo() { MachineId = Model.MachineDetails.AssetTag } };
        return base.OnInitializedAsync();
    }

    private async void Submit()
    {
        systemConfigurationService.SystemConfiguration = Model;
        var isSucceeded = await systemConfigurationService.LogSystemConfiguration();
        if (isSucceeded)
        {
            navigationManager.NavigateTo("/");
        }
        else
        {
            Console.WriteLine("SystemConfiguration upload failed");
        }
    }

    private void AutoFill()
    {
        Model.Motherboards = new List<MotherboardInfo> { SystemConfigurationService.GetMotherboardDetails() };
    }
}
