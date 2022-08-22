namespace Fluid.Shared.Entities;

public class HardDiskInfo : IHardwareComponentInfo, IEquatable<HardDiskInfo>
{
    public bool Equals(HardDiskInfo other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return OemSerialNo == other.OemSerialNo &&
               Manufacturer == other.Manufacturer &&
               Model == other.Model &&
               MediaType == other.MediaType &&
               BusType == other.BusType &&
               HealthCondition == other.HealthCondition &&
               Size == other.Size &&
               Description == other.Description &&
               MachineId == other.MachineId;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((HardDiskInfo)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(OemSerialNo);
        hashCode.Add(Manufacturer);
        hashCode.Add(Model);
        hashCode.Add((int)MediaType);
        hashCode.Add((int)BusType);
        hashCode.Add((int)HealthCondition);
        hashCode.Add(Size);
        hashCode.Add(Description);
        hashCode.Add(MachineId);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(HardDiskInfo left, HardDiskInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(HardDiskInfo left, HardDiskInfo right)
    {
        return !Equals(left, right);
    }

    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public HardwareChange HardwareChange { get; set; }
    public UseStatus UseStatus { get; set; }

    public DriveMediaType MediaType { get; set; }

    public DriveBusType BusType { get; set; }

    public DriveHealthCondition HealthCondition { get; set; }

    public int Size { get; set; }
    public string Description { get; set; }
    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType => ComponentType.HardDisk;
}
