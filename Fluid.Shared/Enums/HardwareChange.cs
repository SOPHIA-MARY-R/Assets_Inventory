using System.ComponentModel;

namespace Fluid.Shared.Enums;

public enum HardwareChange
{
    [Description("Accept from Logs")]
    FromFeedLog,
    [Description("Accept from Machine Master")]
    FromMachineMaster
}