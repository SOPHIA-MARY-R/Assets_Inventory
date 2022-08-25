namespace Fluid.Shared.Entities;

public class PurchaseInfo
{
    [Key]
    public string InvoiceNo { get; set; }

    public DateTime PurchaseDate { get; set; }

    public string VendorName { get; set; }

    public string Salesman { get; set; }

    public string BoughtThrough { get; set; }

    public List<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

    public decimal GrandTotal => PurchaseItems.Select(x => x.NetRate * x.Quantity).Sum();

    public string TechnicianUserId { get; set; }
}
