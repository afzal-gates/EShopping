namespace Inventory.Application.Responses;

public class AutoReorderRuleResponse
{
    public int Id { get; set; }
    public int InventoryItemId { get; set; }
    public string ProductId { get; set; }
    public string SKU { get; set; }
    public bool IsEnabled { get; set; }
    public int ReorderPoint { get; set; }
    public int ReorderQuantity { get; set; }
    public int SafetyStockLevel { get; set; }
    public int MaxStockLevel { get; set; }
    public int PreferredSupplierId { get; set; }
    public string PreferredSupplierName { get; set; }
    public int LeadTimeDays { get; set; }
    public decimal UnitCost { get; set; }
    public int MinimumOrderQuantity { get; set; }
    public bool AutoCreatePO { get; set; }
    public bool RequiresApproval { get; set; }
    public decimal MaxAutoOrderValue { get; set; }
    public bool SendEmailAlert { get; set; }
    public string? AlertEmail { get; set; }
    public DateTime? LastCheckDate { get; set; }
    public DateTime? LastReorderDate { get; set; }
}
