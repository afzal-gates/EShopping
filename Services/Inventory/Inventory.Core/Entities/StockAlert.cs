using Inventory.Core.Enums;

namespace Inventory.Core.Entities;

public class StockAlert : EntityBase
{
    public int InventoryItemId { get; set; }
    public string SKU { get; set; }
    public string ProductName { get; set; }

    // Alert Details
    public AlertStatus Status { get; set; }
    public int CurrentQuantity { get; set; }
    public int ThresholdQuantity { get; set; }
    public string AlertType { get; set; }  // LowStock, OutOfStock, ReorderPoint

    // Notification
    public string? NotificationSentTo { get; set; }
    public DateTime? NotificationSentDate { get; set; }
    public DateTime? ResolvedDate { get; set; }
    public string? ResolvedBy { get; set; }

    public string? Notes { get; set; }
}
