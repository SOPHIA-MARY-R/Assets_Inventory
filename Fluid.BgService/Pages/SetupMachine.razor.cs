using Fluid.BgService.Models;

namespace Fluid.BgService.Pages;

public partial class SetupMachine
{
    private bool _redirectToSetupDetails;
    protected override void OnInitialized()
    {
        if (machineIdentifierService.MachineIdentifier.AssetTag != "unset")
        {
            Model.AssetTag = machineIdentifierService.MachineIdentifier.AssetTag;
        }
        else
        {
            _redirectToSetupDetails = true;
        }
        base.OnInitialized();
    }

    private SetupMachineModel Model { get; set; } = new();

    private async void Submit()
    {
        if (string.IsNullOrWhiteSpace(Model.AssetTag))
        {
            snackbar.Add("Asset Tag cannot be set empty");
        }
        else
        {
            machineIdentifierService.SetAssetTag(Model.AssetTag);
            snackbar.Add("Saved the Asset Tag successfully", MudBlazor.Severity.Success);
            await Task.Delay(1000);
            navigationManager.NavigateTo(_redirectToSetupDetails ? "/Setup-Details" : "/", true);
        }
    }
}
