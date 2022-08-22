using Fluid.Shared.Entities;

namespace Fluid.Shared.Contracts;

public interface IHardwareComponentInfo : IEntity
{
    string OemSerialNo { get; set; }
    
    string Description { get; set; }
    
    HardwareChange HardwareChange { get; set; }
    
    UseStatus UseStatus { get; set; }
    
    MachineInfo? Machine { get; set; }
    
    string? MachineId { get; set; }
    
    DateTime? PurchaseDate { get; set; }
    
    decimal Price { get; set; }
}