namespace Fluid.Shared.Entities;

public class PhysicalMemoryInfo : IHardwareComponentInfo, IEquatable<PhysicalMemoryInfo>
{
    public bool Equals(PhysicalMemoryInfo other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return OemSerialNo == other.OemSerialNo &&
               Manufacturer == other.Manufacturer &&
               Capacity == other.Capacity &&
               Speed.Equals(other.Speed) &&
               MemoryType == other.MemoryType &&
               FormFactor == other.FormFactor &&
               MachineId == other.MachineId;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((PhysicalMemoryInfo)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OemSerialNo, Manufacturer, Capacity, Speed, (int)MemoryType, (int)FormFactor, MachineId, Description);
    }

    public static bool operator ==(PhysicalMemoryInfo left, PhysicalMemoryInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(PhysicalMemoryInfo left, PhysicalMemoryInfo right)
    {
        return !Equals(left, right);
    }

    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }
    public HardwareChangeMode HardwareChangeMode { get; set; }
    public UseStatus UseStatus { get; set; }

    public int Capacity { get; set; }

    public double Speed { get; set; }

    public MemoryType MemoryType { get; set; }

    public MemoryFormFactor FormFactor { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public string Description { get; set; }
}
