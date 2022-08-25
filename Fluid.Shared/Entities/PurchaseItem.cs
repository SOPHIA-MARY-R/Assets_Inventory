namespace Fluid.Shared.Entities;

public class PurchaseItem
{
    [Key]
    public string HSN { get; set; }

    public string SerialNumber { get; set; }

    public string Manufacturer { get; set; }

    public string ItemName { get; set; }

    public int Quantity { get; set; }

    public decimal NetRate { get; set; }

    public decimal Price { get; set; }

    public decimal Amount => Price * Quantity;

    public PurchaseInfo PurchaseInfo { get; set; }

    public string PurchaseInfoId { get; set; }
}
