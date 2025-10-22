using MediatR;

namespace Inventory.Application.Commands;

public class ReserveStockCommand : IRequest<bool>
{
    public string SKU { get; set; }
    public int Quantity { get; set; }
    public string? OrderId { get; set; }
    public string? ReservedBy { get; set; }
}
