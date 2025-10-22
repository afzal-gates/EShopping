namespace Inventory.Application.Responses;

public class InventoryItemResponse
{
    public int Id { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }
    public int QuantityOnHand { get; set; }
    public int QuantityReserved { get; set; }
    public int QuantityAvailable { get; set; }
    public int ReorderPoint { get; set; }
    public int ReorderQuantity { get; set; }
    public int LowStockThreshold { get; set; }
    public string? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public string? Location { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastStockUpdateDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedDate { get; set; }
}
