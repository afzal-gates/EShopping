using MediatR;

namespace Inventory.Application.Commands;

public class AdjustStockCommand : IRequest<bool>
{
    public string SKU { get; set; }
    public int NewQuantity { get; set; }
    public string Reason { get; set; }
    public string? Notes { get; set; }
    public string? ModifiedBy { get; set; }
}
