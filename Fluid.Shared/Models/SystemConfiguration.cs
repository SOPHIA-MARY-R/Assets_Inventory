namespace Fluid.Shared.Models;

public class SystemConfiguration
{
    public SystemConfiguration()
    {
        MachineModel = new();
        Motherboard = new();
        PhysicalMemories = new();
        HardDisks = new();
        Processors = new();
        Mouse = new();
        Keyboard = new();
        Monitor = new();
    }

    public MachineMasterModel MachineModel { get; set; }

    public MotherboardModel Motherboard { get; set; }

    public List<PhysicalMemoryModel> PhysicalMemories { get; set; }

    public List<HardDiskModel> HardDisks { get; set; }

    public List<ProcessorModel> Processors { get; set; }

    public MouseModel Mouse { get; set; }

    public KeyboardModel Keyboard { get; set; }

    public MonitorModel Monitor { get; set; }
}
