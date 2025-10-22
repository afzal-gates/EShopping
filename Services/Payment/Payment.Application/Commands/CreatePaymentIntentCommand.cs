using Payment.Application.Responses;
using MediatR;

namespace Payment.Application.Commands;

public class CreatePaymentIntentCommand : IRequest<PaymentIntentResponse>
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentGateway { get; set; } // Stripe, PayPal
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public string? Description { get; set; }
    public string? CustomerIP { get; set; }
    public string? UserAgent { get; set; }
}
