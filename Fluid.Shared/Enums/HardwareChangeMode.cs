using System.ComponentModel;

namespace Fluid.Shared.Enums;

public enum HardwareChangeMode : byte
{
    [Description("Unchanged")]
    Unchanged,
    [Description("Attached Existing")]
    AddedExisting,
    [Description("New Component!")]
    AddedNew,
    [Description("Detached")]
    Deleted
}