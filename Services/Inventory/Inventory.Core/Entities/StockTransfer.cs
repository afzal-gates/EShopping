namespace Inventory.Core.Entities;

/// <summary>
/// Tracks inventory transfers between warehouses
/// </summary>
public class StockTransfer : EntityBase
{
    public string TransferNumber { get; set; }

    // Warehouse details
    public int FromWarehouseId { get; set; }
    public string FromWarehouseName { get; set; }
    public int ToWarehouseId { get; set; }
    public string ToWarehouseName { get; set; }

    // Product details
    public int InventoryItemId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }

    // Transfer details
    public int Quantity { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, InTransit, Completed, Cancelled
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    public DateTime? ShipDate { get; set; }
    public DateTime? ExpectedArrivalDate { get; set; }
    public DateTime? ActualArrivalDate { get; set; }
    public DateTime? CompletedDate { get; set; }

    // Tracking
    public string? TrackingNumber { get; set; }
    public string? CarrierName { get; set; }
    public decimal? ShippingCost { get; set; }

    // Request details
    public string RequestedBy { get; set; }
    public string Reason { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }

    // Receiving details
    public string? ReceivedBy { get; set; }
    public int? QuantityReceived { get; set; }
    public int? QuantityDamaged { get; set; }
    public string? ReceiverNotes { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public Warehouse FromWarehouse { get; set; }
    public Warehouse ToWarehouse { get; set; }
    public InventoryItem InventoryItem { get; set; }
}
