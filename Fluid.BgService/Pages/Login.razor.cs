using Fluid.Shared.Requests;
using MudBlazor;

namespace Fluid.BgService.Pages;

public partial class Login
{
    private LoginRequest _requestModel = new();

    private async Task SubmitAsync()
    {
        var result = await userHttpClient.Login(_requestModel);
        if (result.Succeeded)
        {
            StateHasChanged();
            if (machineIdentifierService.MachineIdentifier.AssetTag != "unset")
            {
                navigationManager.NavigateTo("/", true);
                snackbar.Add($"Welcome {_requestModel.UserName}", Severity.Success);
            }
            else
            {
                navigationManager.NavigateTo("/Setup-Machine", true);
            }
        }
        else
        {
            foreach (var message in result.Messages)
            {
                snackbar.Add(message, Severity.Error);
            }
        }
    }

    private bool _passwordVisibility;
    private InputType _passwordInput = InputType.Password;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;

    void TogglePasswordVisibility()
    {
        if (_passwordVisibility)
        {
            _passwordVisibility = false;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _passwordVisibility = true;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }
}