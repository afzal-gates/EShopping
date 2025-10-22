namespace Supplier.Core.Entities;

public class Supplier : EntityBase
{
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string? TaxId { get; set; }
    public string? PaymentTerms { get; set; }
    public bool IsActive { get; set; } = true;
    public int Rating { get; set; } = 0;
    public string? Notes { get; set; }

    public ICollection<SupplierProduct> Products { get; set; } = new List<SupplierProduct>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
