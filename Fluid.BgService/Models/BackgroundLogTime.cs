namespace Fluid.BgService.Models;

public class BackgroundLogTime
{
    public DateTime LastLoggedDateTime { get; set; }
    public DateTime NextLogDateTime { get; set; }
    public int CoolDownMinutes { get; set; }
    public int RetryCoolDownMinutes { get; set; }
}