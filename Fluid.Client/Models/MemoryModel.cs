using Fluid.Shared.Enums.Technical;

namespace Fluid.Client.Models
{
    public class MemoryModel
    {
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public int CapacityInGB { get; set; }
        public MemoryType MemoryType { get; set; }
        public double Speed { get; set; }
        public MemoryFormFactor FormFactor { get; set; }
    }
}
