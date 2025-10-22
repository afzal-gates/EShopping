namespace Payment.Core.Entities;

/// <summary>
/// Payment refund tracking
/// </summary>
public class Refund : EntityBase
{
    public int PaymentTransactionId { get; set; }
    public string RefundId { get; set; } // External gateway refund ID

    // Refund details
    public decimal RefundAmount { get; set; }
    public string Currency { get; set; } = "USD";
    public string Reason { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Succeeded, Failed, Cancelled

    // Timing
    public DateTime RefundRequestDate { get; set; } = DateTime.UtcNow;
    public DateTime? RefundCompletedDate { get; set; }
    public int EstimatedDaysToRefund { get; set; } = 5;

    // Request details
    public string RequestedBy { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }

    // Gateway response
    public string? GatewayResponse { get; set; }
    public string? FailureReason { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public PaymentTransaction PaymentTransaction { get; set; }
}
