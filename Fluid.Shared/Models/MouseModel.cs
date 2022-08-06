namespace Fluid.Shared.Models;
using Fluid.Shared.Entities;

public class MouseModel
{
    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public bool IsWireless { get; set; }

    public string Description { get; set; }

    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType => ComponentType.Mouse;
}
