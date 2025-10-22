using Inventory.Core.Enums;

namespace Inventory.Core.Entities;

public class StockMovement : EntityBase
{
    public int InventoryItemId { get; set; }
    public string SKU { get; set; }
    public string ProductId { get; set; }
    public string? VariationId { get; set; }

    // Movement Details
    public StockMovementType MovementType { get; set; }
    public int Quantity { get; set; }
    public int PreviousQuantity { get; set; }
    public int NewQuantity { get; set; }

    // Reference Information
    public string? ReferenceType { get; set; }  // Order, PO, Transfer, etc.
    public string? ReferenceId { get; set; }

    // Warehouse/Location
    public string? FromWarehouseId { get; set; }
    public string? ToWarehouseId { get; set; }
    public string? Location { get; set; }

    // Audit
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public DateTime MovementDate { get; set; } = DateTime.UtcNow;
}
