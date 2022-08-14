using System.ComponentModel;

namespace Fluid.Shared.Enums.Technical;
public enum SpeakerInputPorts: byte
{
    [Description("Other")]
    Other = 1, 
    [Description("3.5 mm")]
    _3Point5mm = 2,
    [Description("USB")]
    USB = 3, 
}
