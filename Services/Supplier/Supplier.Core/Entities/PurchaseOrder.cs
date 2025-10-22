using Supplier.Core.Enums;

namespace Supplier.Core.Entities;

public class PurchaseOrder : EntityBase
{
    public string PONumber { get; set; }
    public int SupplierId { get; set; }
    public POStatus Status { get; set; } = POStatus.Draft;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpectedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public decimal TotalCost { get; set; }
    public string Currency { get; set; } = "USD";
    public string? Notes { get; set; }

    public Supplier Supplier { get; set; }
    public ICollection<POItem> Items { get; set; } = new List<POItem>();
}

public class POItem : EntityBase
{
    public int PurchaseOrderId { get; set; }
    public string ProductId { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalCost { get; set; }
    public int ReceivedQuantity { get; set; } = 0;

    public PurchaseOrder PurchaseOrder { get; set; }
}
