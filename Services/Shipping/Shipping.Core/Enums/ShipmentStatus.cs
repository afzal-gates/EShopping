namespace Shipping.Core.Enums;

public enum ShipmentStatus
{
    Pending = 1,
    PickedUp = 2,
    InTransit = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Exception = 6,
    Returned = 7,
    Cancelled = 8
}
