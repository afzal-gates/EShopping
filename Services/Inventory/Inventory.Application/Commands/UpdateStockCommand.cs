using MediatR;

namespace Inventory.Application.Commands;

public class UpdateStockCommand : IRequest<bool>
{
    public string SKU { get; set; }
    public int QuantityChange { get; set; }  // Positive for add, negative for remove
    public string Reason { get; set; }
    public string? ReferenceType { get; set; }
    public string? ReferenceId { get; set; }
    public string? Notes { get; set; }
    public string? ModifiedBy { get; set; }
}
