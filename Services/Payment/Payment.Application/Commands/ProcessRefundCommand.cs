using Payment.Application.Responses;
using MediatR;

namespace Payment.Application.Commands;

public class ProcessRefundCommand : IRequest<RefundResponse>
{
    public int PaymentTransactionId { get; set; }
    public decimal RefundAmount { get; set; }
    public string Reason { get; set; }
    public string RequestedBy { get; set; }
    public string? Notes { get; set; }
}
