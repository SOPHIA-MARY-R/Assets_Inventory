namespace Fluid.Shared.Requests;

public class FeedLogFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string AssetTag { get; set; }
    public string MachineName { get; set; }
    public string AssignedPersonName { get; set; }
    public string AssetLocation { get; set; }
    public string AssetBranch { get; set; }
    public long FromDateTimeTicks { get; set; }
    public long ToDateTimeTicks { get; set; }
    public LogAttendStatus? LogAttendStatus { get; set; }
    public MachineType? MachineType { get; set; }
}