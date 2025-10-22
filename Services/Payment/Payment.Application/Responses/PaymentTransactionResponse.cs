namespace Payment.Application.Responses;

public class PaymentTransactionResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string OrderNumber { get; set; }
    public string PaymentGateway { get; set; }
    public string PaymentMethod { get; set; }
    public string TransactionId { get; set; }
    public string PaymentIntentId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Status { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? CardBrand { get; set; }
    public string? CardLast4 { get; set; }
    public string? FailureReason { get; set; }
}

public class RefundResponse
{
    public int Id { get; set; }
    public int PaymentTransactionId { get; set; }
    public string RefundId { get; set; }
    public decimal RefundAmount { get; set; }
    public string Currency { get; set; }
    public string Reason { get; set; }
    public string Status { get; set; }
    public DateTime RefundRequestDate { get; set; }
    public int EstimatedDaysToRefund { get; set; }
}
