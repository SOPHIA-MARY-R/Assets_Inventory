namespace Fluid.Shared.Entities;

public class MachineInfo : IEntity, IEquatable<MachineInfo>
{
    public bool Equals(MachineInfo other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return AssetTag == other.AssetTag &&
               OemSerialNo == other.OemSerialNo &&
               MachineName == other.MachineName &&
               Model == other.Model &&
               Manufacturer == other.Manufacturer &&
               MachineType == other.MachineType &&
               UseType == other.UseType &&
               AssignedPersonName == other.AssignedPersonName &&
               AssetLocation == other.AssetLocation &&
               AssetBranch == other.AssetBranch;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((MachineInfo)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(AssetTag);
        hashCode.Add(OemSerialNo);
        hashCode.Add(MachineName);
        hashCode.Add(Model);
        hashCode.Add(Manufacturer);
        hashCode.Add((int)MachineType);
        hashCode.Add((int)UseType);
        hashCode.Add(AssignedPersonName);
        hashCode.Add(AssetLocation);
        hashCode.Add(AssetBranch);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(MachineInfo left, MachineInfo right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(MachineInfo left, MachineInfo right)
    {
        return !Equals(left, right);
    }

    [Key]
    public string AssetTag { get; set; } //Primary Key

    public string OemSerialNo { get; set; }

    public string MachineName { get; set; }

    public string Model { get; set; }

    public string Manufacturer { get; set; }

    public MachineType MachineType { get; set; }

    public MachineUseType UseType { get; set; }

    public UseStatus UseStatus { get; set; }

    public string AssignedPersonName { get; set; }

    public string AssetLocation { get; set; }

    public string AssetBranch { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public DateTime? InitializationDate { get; set; }

    public decimal Price { get; set; }
}
