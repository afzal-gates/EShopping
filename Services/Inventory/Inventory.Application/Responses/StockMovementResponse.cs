namespace Inventory.Application.Responses;

public class StockMovementResponse
{
    public int Id { get; set; }
    public int InventoryItemId { get; set; }
    public string SKU { get; set; }
    public string ProductId { get; set; }
    public string? VariationId { get; set; }
    public string MovementType { get; set; }
    public int Quantity { get; set; }
    public int PreviousQuantity { get; set; }
    public int NewQuantity { get; set; }
    public string? ReferenceType { get; set; }
    public string? ReferenceId { get; set; }
    public string? FromWarehouseId { get; set; }
    public string? ToWarehouseId { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public DateTime MovementDate { get; set; }
    public string? CreatedBy { get; set; }
}
