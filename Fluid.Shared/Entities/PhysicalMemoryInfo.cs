namespace Fluid.Shared.Entities;

public class PhysicalMemoryInfo : IEntity
{
    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }

    public UseStatus UseStatus { get; set; }

    public int Capacity { get; set; }

    public double Speed { get; set; }

    public MemoryType MemoryType { get; set; }

    public MemoryFormFactor FormFactor { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType { get; set; }
}
