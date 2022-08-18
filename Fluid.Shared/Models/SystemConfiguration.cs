using Fluid.Shared.Entities;

namespace Fluid.Shared.Models;

public class SystemConfiguration
{
    public MachineInfo MachineDetails { get; set; } = new();

    public MotherboardInfo Motherboard { get; set; } = new();

    public List<PhysicalMemoryInfo> PhysicalMemories { get; set; } = new();

    public List<HardDiskInfo> HardDisks { get; set; } = new();

    public List<ProcessorInfo> Processors { get; set; } = new();

    public MouseInfo Mouse { get; set; } = new();

    public KeyboardInfo Keyboard { get; set; } = new();

    public MonitorInfo Monitor { get; set; } = new();
}