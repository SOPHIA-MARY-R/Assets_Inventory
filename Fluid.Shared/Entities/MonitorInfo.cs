namespace Fluid.Shared.Entities;

public class MonitorInfo : IHardwareComponentInfo, IEquatable<MonitorInfo>
{
    public bool Equals(MonitorInfo other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return OemSerialNo == other.OemSerialNo &&
               Manufacturer == other.Manufacturer &&
               Model == other.Model &&
               PanelType == other.PanelType &&
               RefreshRate == other.RefreshRate &&
               HasBuiltInSpeakers == other.HasBuiltInSpeakers &&
               HDMIPortCount == other.HDMIPortCount &&
               VGAPortCount == other.VGAPortCount &&
               Description == other.Description &&
               MachineId == other.MachineId;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((MonitorInfo)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(OemSerialNo);
        hashCode.Add(Manufacturer);
        hashCode.Add(Model);
        hashCode.Add((int)PanelType);
        hashCode.Add(RefreshRate);
        hashCode.Add(HasBuiltInSpeakers);
        hashCode.Add(HDMIPortCount);
        hashCode.Add(VGAPortCount);
        hashCode.Add(Description);
        hashCode.Add(MachineId);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(MonitorInfo left, MonitorInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MonitorInfo left, MonitorInfo right)
    {
        return !Equals(left, right);
    }

    [Key]
    public string OemSerialNo { get; set; }

    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public MonitorPanelType PanelType { get; set; }
    public decimal RefreshRate { get; set; }
    public bool HasBuiltInSpeakers { get; set; }
    public int HDMIPortCount { get; set; }
    public int VGAPortCount { get; set; }
    public string Description { get; set; }
    public HardwareChange HardwareChange { get; set; }
    public string Version { get; set; }
    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType => ComponentType.Monitor;
}
