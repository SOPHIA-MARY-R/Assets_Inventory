namespace Fluid.Shared.Entities;

public class CameraInfo : IEntity
{
    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public int MegaPixels { get; set; }
    public CameraResolution Resolution { get; set; }
    public bool HasBuiltInMic { get; set; }
    public bool IsWireLess { get; set; }
    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType => ComponentType.Camera;
}