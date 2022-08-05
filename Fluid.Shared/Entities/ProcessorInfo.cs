namespace Fluid.Shared.Entities;

public class ProcessorInfo
{
    public string ProcessorId { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public ProcessorArchitecture Architecture { get; set; }
    public int Family { get; set; }
    public int NumberOfCores { get; set; }
    public int NumberOfLogicalProcessors { get; set; }
    public int ThreadCount { get; set; }
    public int MaxClockSpeed { get; set; }
}
