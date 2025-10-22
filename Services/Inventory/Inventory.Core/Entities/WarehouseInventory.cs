namespace Inventory.Core.Entities;

/// <summary>
/// Tracks inventory for each product at each warehouse
/// </summary>
public class WarehouseInventory : EntityBase
{
    public int WarehouseId { get; set; }
    public int InventoryItemId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }

    // Stock levels per warehouse
    public int QuantityOnHand { get; set; }
    public int QuantityReserved { get; set; }
    public int QuantityAvailable => QuantityOnHand - QuantityReserved;
    public int QuantityInTransit { get; set; }

    // Warehouse-specific settings
    public int MinStockLevel { get; set; }
    public int MaxStockLevel { get; set; }
    public string? BinLocation { get; set; }
    public string? AisleLocation { get; set; }
    public string? ShelfLocation { get; set; }

    // Status
    public bool IsActive { get; set; } = true;
    public DateTime? LastStockUpdateDate { get; set; }
    public DateTime? LastCountDate { get; set; }

    // Cost tracking per warehouse
    public decimal AverageCost { get; set; }
    public decimal? LastPurchaseCost { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public Warehouse Warehouse { get; set; }
    public InventoryItem InventoryItem { get; set; }
}
