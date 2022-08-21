using Fluid.Shared.Entities;

namespace Fluid.Shared.Models;

public class SystemConfiguration : IEquatable<SystemConfiguration>
{
    public bool Equals(SystemConfiguration other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(MachineDetails,
                   other.MachineDetails) &&
               Equals(Motherboards,
                   other.Motherboards) &&
               Equals(PhysicalMemories,
                   other.PhysicalMemories) &&
               Equals(HardDisks,
                   other.HardDisks) &&
               Equals(Processors,
                   other.Processors) &&
               Equals(Mouses,
                   other.Mouses) &&
               Equals(Keyboards,
                   other.Keyboards) &&
               Equals(Monitors,
                   other.Monitors);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((SystemConfiguration)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MachineDetails,
            Motherboards,
            PhysicalMemories,
            HardDisks,
            Processors,
            Mouses,
            Keyboards,
            Monitors);
    }

    public static bool operator ==(SystemConfiguration left, SystemConfiguration right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SystemConfiguration left, SystemConfiguration right)
    {
        return !Equals(left, right);
    }

    public MachineInfo MachineDetails { get; set; } = new();

    public List<MotherboardInfo> Motherboards { get; set; } = new();

    public List<PhysicalMemoryInfo> PhysicalMemories { get; set; } = new();

    public List<HardDiskInfo> HardDisks { get; set; } = new();

    public List<ProcessorInfo> Processors { get; set; } = new();

    public List<MouseInfo> Mouses { get; set; } = new();

    public List<KeyboardInfo> Keyboards { get; set; } = new();

    public List<MonitorInfo> Monitors { get; set; } = new();
}