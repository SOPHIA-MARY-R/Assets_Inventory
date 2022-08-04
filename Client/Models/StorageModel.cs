namespace MyFirstBlazorWASMApp.Client.Models
{
    public class StorageModel
    {
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public DriveBusType BusType { get; set; }
        public DriveMediaType MediaType { get; set; }
        public DriveHealthCondition HealthCondition { get; set; }
        public string FriendlyName { get; set; }
        public int Size { get; set; }
        public bool IsActive { get; set; }
    }
}
