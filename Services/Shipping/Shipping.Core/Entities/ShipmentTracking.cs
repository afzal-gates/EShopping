using Shipping.Core.Enums;

namespace Shipping.Core.Entities;

public class ShipmentTracking : EntityBase
{
    public int ShipmentId { get; set; }
    public ShipmentStatus Status { get; set; }
    public string Location { get; set; }
    public DateTime Timestamp { get; set; }
    public string Description { get; set; }
    public string? CarrierStatus { get; set; }

    // Navigation
    public Shipment Shipment { get; set; }
}
