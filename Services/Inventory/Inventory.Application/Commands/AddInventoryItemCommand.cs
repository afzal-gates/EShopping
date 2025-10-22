using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Commands;

public class AddInventoryItemCommand : IRequest<InventoryItemResponse>
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }
    public int QuantityOnHand { get; set; }
    public int ReorderPoint { get; set; } = 10;
    public int ReorderQuantity { get; set; } = 50;
    public int LowStockThreshold { get; set; } = 5;
    public string? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public string? CreatedBy { get; set; }
}
