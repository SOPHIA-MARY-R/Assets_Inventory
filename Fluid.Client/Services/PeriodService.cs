namespace Fluid.Client.Services;

public class PeriodService
{
    public DateTime FromDate { get; set; } = DateTime.Now.AddDays(1 - DateTime.Now.Day)
                                                .AddHours(0 - DateTime.Now.Hour)
                                                .AddMinutes(0 - DateTime.Now.Minute)
                                                .AddSeconds(0 - DateTime.Now.Second);
    public DateTime ToDate { get; set; } = DateTime.Now.AddHours(23 - DateTime.Now.Hour)
                                                .AddMinutes(59 - DateTime.Now.Minute)
                                                .AddSeconds(59 - DateTime.Now.Second);
}