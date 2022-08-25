namespace Fluid.Shared.Entities;

public class CameraInfo : IHardwareComponentInfo, IEquatable<CameraInfo>
{
    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public HardwareChangeMode HardwareChangeMode { get; set; }
    public int MegaPixels { get; set; }
    public CameraResolution Resolution { get; set; }
    public bool HasBuiltInMic { get; set; }
    public bool IsWireLess { get; set; }
    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public bool Equals(CameraInfo other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return OemSerialNo == other.OemSerialNo && Manufacturer == other.Manufacturer && Model == other.Model && MegaPixels == other.MegaPixels && Resolution == other.Resolution && HasBuiltInMic == other.HasBuiltInMic && IsWireLess == other.IsWireLess && MachineId == other.MachineId;
    }

    public static bool operator ==(CameraInfo left, CameraInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CameraInfo left, CameraInfo right)
    {
        return !Equals(left, right);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CameraInfo)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(OemSerialNo);
        hashCode.Add(Manufacturer);
        hashCode.Add(Model);
        hashCode.Add(MegaPixels);
        hashCode.Add((int)Resolution);
        hashCode.Add(HasBuiltInMic);
        hashCode.Add(IsWireLess);
        hashCode.Add(MachineId);
        return hashCode.ToHashCode();
    }
}