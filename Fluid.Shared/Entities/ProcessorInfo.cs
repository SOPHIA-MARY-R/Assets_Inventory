namespace Fluid.Shared.Entities;

public class ProcessorInfo : IEntity, IEquatable<ProcessorInfo>
{
    public bool Equals(ProcessorInfo other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ProcessorId == other.ProcessorId &&
               Name == other.Name &&
               Manufacturer == other.Manufacturer &&
               Architecture == other.Architecture &&
               Family == other.Family &&
               NumberOfCores == other.NumberOfCores &&
               NumberOfLogicalProcessors == other.NumberOfLogicalProcessors &&
               ThreadCount == other.ThreadCount &&
               MaxClockSpeed == other.MaxClockSpeed &&
               MachineId == other.MachineId;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((ProcessorInfo)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(ProcessorId);
        hashCode.Add(Name);
        hashCode.Add(Manufacturer);
        hashCode.Add((int)Architecture);
        hashCode.Add(Family);
        hashCode.Add(NumberOfCores);
        hashCode.Add(NumberOfLogicalProcessors);
        hashCode.Add(ThreadCount);
        hashCode.Add(MaxClockSpeed);
        hashCode.Add(MachineId);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(ProcessorInfo left, ProcessorInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ProcessorInfo left, ProcessorInfo right)
    {
        return !Equals(left, right);
    }

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
    public HardwareChange HardwareChange { get; set; }

    public UseStatus UseStatus { get; set; }

    public MachineInfo? Machine { get; set; }
    public string? MachineId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public decimal Price { get; set; }

    public ComponentType ComponentType => ComponentType.Processor;
}
