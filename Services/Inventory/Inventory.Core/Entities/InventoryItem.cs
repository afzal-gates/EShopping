namespace Inventory.Core.Entities;

public class InventoryItem : EntityBase
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }

    // Stock Information
    public int QuantityOnHand { get; set; }
    public int QuantityReserved { get; set; }
    public int QuantityAvailable => QuantityOnHand - QuantityReserved;

    // Alert Thresholds
    public int ReorderPoint { get; set; } = 10;
    public int ReorderQuantity { get; set; } = 50;
    public int LowStockThreshold { get; set; } = 5;

    // Warehouse Information
    public string? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public string? Location { get; set; }

    // Status
    public bool IsActive { get; set; } = true;
    public DateTime? LastStockUpdateDate { get; set; }

    // Additional Info
    public string? Notes { get; set; }
}
