namespace Fluid.Shared.Entities;

public class FeedLog : IEntity
{
    [Key]
    public string Id { get; set; }

    public string AssetTag { get; set; }

    public string OemSerialNo { get; set; }

    public string MachineName { get; set; }

    public string Model { get; set; }

    public string Manufacturer { get; set; }

    public MachineType MachineType { get; set; }

    public string AssignedPersonName { get; set; }

    public string AssetLocation { get; set; }

    public string AssetBranch { get; set; }

    public string JsonRaw { get; set; }

    public DateTime LogDateTime { get; set; }
}
