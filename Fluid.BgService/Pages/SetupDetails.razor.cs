using Fluid.Shared.Models;

namespace Fluid.BgService.Pages;

public partial class SetupDetails
{
    public SystemConfiguration Model = new();

    public void Submit()
    {
        systemConfigurationService.SetSystemConfiguration(Model);
    }

    protected override Task OnInitializedAsync()
    {
        Model.MachineModel.AssetTag = machineIdentifierService.MachineIdentifier.AssetTag;
        Model.Motherboard.MachineId = Model.MachineModel.AssetTag;
        Model.Mouse.MachineId = Model.MachineModel.AssetTag;
        Model.Keyboard.MachineId = Model.MachineModel.AssetTag;
        return base.OnInitializedAsync();
    }
}
