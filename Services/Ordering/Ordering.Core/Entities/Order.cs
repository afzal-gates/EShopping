using Ordering.Core.Common;
using Ordering.Core.Enums;

namespace Ordering.Core.Entities;

public class Order : EntityBase
{
    public string? UserName { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? AddressLine { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? Expiration { get; set; }
    public string? Cvv { get; set; }
    public int? PaymentMethod { get; set; }

    // Order Status Workflow
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime? PaymentDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public DateTime? CancelledDate { get; set; }
    public string? CancellationReason { get; set; }
    public string? TrackingNumber { get; set; }
    public string? CarrierName { get; set; }

    // Navigation
    public ICollection<OrderStatusHistory> StatusHistory { get; set; } = new List<OrderStatusHistory>();
}