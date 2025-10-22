using Ordering.Core.Common;
using Ordering.Core.Enums;

namespace Ordering.Core.Entities;

public class OrderStatusHistory : EntityBase
{
    public int OrderId { get; set; }
    public OrderStatus FromStatus { get; set; }
    public OrderStatus ToStatus { get; set; }
    public DateTime StatusChangeDate { get; set; } = DateTime.UtcNow;
    public string? ChangedBy { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public Order Order { get; set; }
}
