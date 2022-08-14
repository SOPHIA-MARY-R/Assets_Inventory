using System.ComponentModel;

namespace Fluid.Shared.Enums.Technical
{
    public enum CameraResolution : byte
    {
        [Description("320p")]
        _320p = 0,
        [Description("480p")]
        _480p = 1,
        [Description("720p")]
        _720p = 2,
        [Description("1080p")]
        _1080p = 3,
        [Description("Other")]
        Other = 4
    }
}
