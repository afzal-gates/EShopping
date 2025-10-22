using MediatR;

namespace Ordering.Application.Commands;

public class CancelOrderCommand : IRequest<bool>
{
    public int OrderId { get; set; }
    public string Reason { get; set; }
    public string? CancelledBy { get; set; }
}
