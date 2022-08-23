using System.ComponentModel;

namespace Fluid.Shared.Enums.Technical
{
    public enum ProcessorFamily : byte
    {
        [Description("Other")]
        Other = 1,
        [Description("Unknown")]
        Unknown = 2,
        [Description("8086")]
        _8086 = 3,
        [Description("80286")]
        _80286 = 4,
        [Description("80386")]
        _80386 = 5,
        [Description("80486")]
        _80486 = 6,
        [Description("8087")]
        _8087 = 7,
        [Description("80287")]
        _80287 = 8,
        [Description("80387")]
        _80387= 9,
        [Description("80487")]
        _80487 = 10,
        [Description("Pentium(R) brand")]
        PentiumR_brand = 11,
        [Description("Pentium(R) Pro")]
        PentiumR_pro = 12,
        [Description("Pentium(R) II")]
        PentiumR_II = 13,
        [Description("Pentium(R) processor with MMX(TM) technology")]
        PentiumR_processor_with_MMXTM_technology = 14,
        [Description("Celeron(TM)")]
        CeleronTM = 15
    }
}
