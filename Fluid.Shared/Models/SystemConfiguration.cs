namespace Fluid.Shared.Models;

public class SystemConfiguration
{
    public MachineMasterModel MachineDetails { get; set; } = new();

    public MotherboardModel Motherboard { get; set; } = new();

    public List<PhysicalMemoryModel> PhysicalMemories { get; set; } = new();

    public List<HardDiskModel> HardDisks { get; set; } = new();

    public List<ProcessorModel> Processors { get; set; } = new();

    public MouseModel Mouse { get; set; } = new();

    public KeyboardModel Keyboard { get; set; } = new();

    public MonitorModel Monitor { get; set; } = new();
}