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

    public SetupMachineModel Model { get; set; } = new();

    public async void Submit()
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
            if (_redirectToSetupDetails)
            {
                navigationManager.NavigateTo("/Setup-Details", true);
            }
            else
            {
                navigationManager.NavigateTo("/", true);
            }
        }
    }
}
