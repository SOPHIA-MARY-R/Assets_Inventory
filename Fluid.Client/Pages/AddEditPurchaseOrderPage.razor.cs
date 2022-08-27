using Fluid.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace Fluid.Client.Pages;

public partial class AddEditPurchaseOrderPage
{
    [Parameter] public string Id { get; set; }
    
    private PurchaseInfo Model { get; set; }

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }
}