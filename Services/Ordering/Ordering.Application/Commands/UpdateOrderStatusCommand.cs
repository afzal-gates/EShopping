using MediatR;
using Ordering.Core.Enums;

namespace Ordering.Application.Commands;

public class UpdateOrderStatusCommand : IRequest<bool>
{
    public int OrderId { get; set; }
    public OrderStatus NewStatus { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public string? ChangedBy { get; set; }
    public string? TrackingNumber { get; set; }
    public string? CarrierName { get; set; }
}
