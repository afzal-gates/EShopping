using Shipping.Core.Enums;

namespace Shipping.Core.Entities;

public class Shipment : EntityBase
{
    public int OrderId { get; set; }
    public string TrackingNumber { get; set; }
    public CarrierCode CarrierCode { get; set; }
    public string CarrierName { get; set; }
    public string ServiceType { get; set; }

    public ShipmentStatus Status { get; set; } = ShipmentStatus.Pending;
    public DateTime? ShipDate { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }

    public decimal ShippingCost { get; set; }
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }

    // From Address
    public string FromName { get; set; }
    public string FromAddressLine1 { get; set; }
    public string? FromAddressLine2 { get; set; }
    public string FromCity { get; set; }
    public string FromState { get; set; }
    public string FromZipCode { get; set; }
    public string FromCountry { get; set; }

    // To Address
    public string ToName { get; set; }
    public string ToAddressLine1 { get; set; }
    public string? ToAddressLine2 { get; set; }
    public string ToCity { get; set; }
    public string ToState { get; set; }
    public string ToZipCode { get; set; }
    public string ToCountry { get; set; }

    public string? LabelUrl { get; set; }
    public string? Notes { get; set; }

    // Navigation
    public ICollection<ShipmentTracking> TrackingHistory { get; set; } = new List<ShipmentTracking>();
}
