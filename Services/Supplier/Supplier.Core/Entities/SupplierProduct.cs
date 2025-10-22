namespace Supplier.Core.Entities;

public class SupplierProduct : EntityBase
{
    public int SupplierId { get; set; }
    public string ProductId { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }
    public string SupplierSKU { get; set; }
    public decimal UnitCost { get; set; }
    public int LeadTimeDays { get; set; }
    public int MinOrderQuantity { get; set; } = 1;
    public bool IsPreferred { get; set; } = false;
    public DateTime? LastPurchaseDate { get; set; }
    public int TotalPurchased { get; set; } = 0;

    public Supplier Supplier { get; set; }
}
