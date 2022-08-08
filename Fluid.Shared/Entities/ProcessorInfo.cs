namespace Fluid.Shared.Entities;

public class ProcessorInfo : IEntity
{
    [Key]
    public string ProcessorId { get; set; }
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public ProcessorArchitecture Architecture { get; set; }
    public int Family { get; set; }
    public int NumberOfCores { get; set; }
    public int NumberOfLogicalProcessors { get; set; }
    public int ThreadCount { get; set; }
    public int MaxClockSpeed { get; set; }
    public string Description { get; set; }

    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType => ComponentType.Processor;
}
