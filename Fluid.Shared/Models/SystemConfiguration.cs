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
               CompareLists(Motherboards, other.Motherboards) &&
               CompareLists(PhysicalMemories, other.PhysicalMemories) &&
               CompareLists(HardDisks, other.HardDisks) &&
               Processors.OrderBy(x => x.ProcessorId).SequenceEqual(other.Processors.OrderBy(x => x.ProcessorId)) &&
               CompareLists(Mouses, other.Mouses) &&
               CompareLists(Keyboards, other.Keyboards) &&
               CompareLists(Monitors, other.Monitors);
    }

    private static bool CompareLists(IEnumerable<IHardwareComponentInfo> list1, IEnumerable<IHardwareComponentInfo> list2)
    {
        var hardwareComponentInfos1 = list1.ToList();
        var hardwareComponentInfos2 = list2.ToList();
        foreach (var serialNo in hardwareComponentInfos1.Select(x => x.OemSerialNo))
            if (hardwareComponentInfos2.Count(x => x.OemSerialNo == serialNo) != 1)
                return false;
        foreach (var serialNo in hardwareComponentInfos2.Select(x => x.OemSerialNo))
            if (hardwareComponentInfos1.Count(x => x.OemSerialNo == serialNo) != 1)
                return false;
        return true;
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