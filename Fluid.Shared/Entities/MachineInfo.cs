namespace Fluid.Shared.Entities;

public class MachineInfo : IEntity
{
    [Key]
    public string OemServiceTag { get; set; } //Primary Key

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
