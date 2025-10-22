namespace Ordering.Core.Enums;

public enum OrderStatus
{
    Pending = 1,         // Order created, payment pending
    PaymentReceived = 2, // Payment confirmed
    Processing = 3,      // Order being prepared
    Packed = 4,          // Order packed, ready for shipment
    Shipped = 5,         // Order shipped to customer
    Delivered = 6,       // Order delivered successfully
    Cancelled = 7,       // Order cancelled
    Refunded = 8,        // Order refunded
    Failed = 9           // Order failed (payment/processing)
}
