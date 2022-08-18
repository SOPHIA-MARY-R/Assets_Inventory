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
               Equals(Motherboard,
                   other.Motherboard) &&
               Equals(PhysicalMemories,
                   other.PhysicalMemories) &&
               Equals(HardDisks,
                   other.HardDisks) &&
               Equals(Processors,
                   other.Processors) &&
               Equals(Mouse,
                   other.Mouse) &&
               Equals(Keyboard,
                   other.Keyboard) &&
               Equals(Monitor,
                   other.Monitor);
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
            Motherboard,
            PhysicalMemories,
            HardDisks,
            Processors,
            Mouse,
            Keyboard,
            Monitor);
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

    public MotherboardInfo Motherboard { get; set; } = new();

    public List<PhysicalMemoryInfo> PhysicalMemories { get; set; } = new();

    public List<HardDiskInfo> HardDisks { get; set; } = new();

    public List<ProcessorInfo> Processors { get; set; } = new();

    public MouseInfo Mouse { get; set; } = new();

    public KeyboardInfo Keyboard { get; set; } = new();

    public MonitorInfo Monitor { get; set; } = new();
}