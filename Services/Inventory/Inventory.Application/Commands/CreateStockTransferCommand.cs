using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Commands;

public class CreateStockTransferCommand : IRequest<StockTransferResponse>
{
    public int FromWarehouseId { get; set; }
    public int ToWarehouseId { get; set; }
    public int InventoryItemId { get; set; }
    public string SKU { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; }
    public string RequestedBy { get; set; }
    public DateTime? ExpectedArrivalDate { get; set; }
    public string? Notes { get; set; }
}
