namespace Fluid.Shared.Models;

public class FeedLogCountDetails
{
    public int TotalLogs { get; set; }
    public int NewLogs { get; set; }
    public int Pending { get; set; }
    public int Accepted { get; set; }
    public int Ignored { get; set; }
    public int NewMachines { get; set; }
}