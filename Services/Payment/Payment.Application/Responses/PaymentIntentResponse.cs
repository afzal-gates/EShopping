namespace Payment.Application.Responses;

public class PaymentIntentResponse
{
    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public string PaymentGateway { get; set; }
    public bool Is3DSecureRequired { get; set; }
}
