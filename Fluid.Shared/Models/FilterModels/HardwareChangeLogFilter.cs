namespace Fluid.Shared.Models.FilterModels;

public class HardwareChangeLogFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string OrderBy { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AssetTag { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public MachineType? MachineType { get; set; }
    public string MachineName { get; set; }
    public string AssetBranch { get; set; }
    public string AssetLocation { get; set; }
    public string AssignedPersonName { get; set; }
}