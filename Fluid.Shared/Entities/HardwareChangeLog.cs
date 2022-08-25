namespace Fluid.Shared.Entities;

public class HardwareChangeLog : IEntity
{
    public string Id { get; set; }
    public DateTime ChangeDateTime { get; set; }
    public string AssetTag { get; set; }

    public string OemSerialNo { get; set; }
    
    public string Model { get; set; }
    
    public string Manufacturer { get; set; }

    public string OldMachineName { get; set; }
    public string OldAssignedPersonName { get; set; }
    public string OldAssetLocation { get; set; }
    public string OldAssetBranch { get; set; }
    
    public string MachineName { get; set; }
    public string AssignedPersonName { get; set; }
    public string AssetLocation { get; set; }
    public string AssetBranch { get; set; }
    public MachineType MachineType { get; set; }
    
    public string OldConfigJsonRaw { get; set; }
    
    public string NewConfigJsonRaw { get; set; }
    
    public string Remarks { get; set; }
}