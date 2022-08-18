using Fluid.Shared.Models;

namespace Fluid.Client.Pages;

public partial class AddEditMachineInfo
{
    public string Id { get; set; }
    private SystemConfiguration _systemConfiguration = new SystemConfiguration();
    protected override Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Id))
        {
            
        }
        return base.OnInitializedAsync();
    }
    
}