using Fluid.Client.Enums;

namespace Fluid.Client.Models
{
    public class ProcessorModel
    {
        public string SerialNumber { get; set; }
        public string ProcessorID { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public ProcessorArchitecture Architecture { get; set; }
        public int Family { get; set; }
        public int NumberOfCores { get; set; }
        public int NumberOfLogicalProcessors { get; set; }
        public int ThreadCount { get; set; }
        public int MaxClockSpeed { get; set; }
    }
}
