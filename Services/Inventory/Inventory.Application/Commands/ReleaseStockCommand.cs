using MediatR;

namespace Inventory.Application.Commands;

public class ReleaseStockCommand : IRequest<bool>
{
    public string SKU { get; set; }
    public int Quantity { get; set; }
    public string? OrderId { get; set; }
    public string? ReleasedBy { get; set; }
}
