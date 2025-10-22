namespace Payment.Core.Entities;

/// <summary>
/// Payment transaction tracking for Stripe/PayPal
/// </summary>
public class PaymentTransaction : EntityBase
{
    // Order reference
    public int OrderId { get; set; }
    public string OrderNumber { get; set; }

    // Payment gateway
    public string PaymentGateway { get; set; } // Stripe, PayPal
    public string PaymentMethod { get; set; } // CreditCard, DebitCard, PayPal, BankTransfer

    // Transaction details
    public string TransactionId { get; set; } // External gateway transaction ID
    public string PaymentIntentId { get; set; } // Stripe PaymentIntent or PayPal Order ID
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";

    // Status
    public string Status { get; set; } = "Pending"; // Pending, Processing, Succeeded, Failed, Cancelled, Refunded
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedDate { get; set; }

    // Customer details
    public string UserId { get; set; }
    public string UserEmail { get; set; }

    // Card details (tokenized - PCI DSS compliant)
    public string? CardBrand { get; set; } // Visa, Mastercard, Amex
    public string? CardLast4 { get; set; }
    public string? CardExpMonth { get; set; }
    public string? CardExpYear { get; set; }

    // PayPal specific
    public string? PayPalPayerId { get; set; }
    public string? PayPalPayerEmail { get; set; }

    // Gateway response
    public string? GatewayResponse { get; set; }
    public string? FailureReason { get; set; }
    public string? FailureCode { get; set; }

    // 3D Secure
    public bool Is3DSecureRequired { get; set; }
    public string? ThreeDSecureStatus { get; set; }

    // Webhook tracking
    public DateTime? WebhookReceivedDate { get; set; }
    public string? WebhookEventId { get; set; }

    // Metadata
    public string? Description { get; set; }
    public string? CustomerIP { get; set; }
    public string? UserAgent { get; set; }

    // Refund relationship
    public ICollection<Refund> Refunds { get; set; } = new List<Refund>();
}
