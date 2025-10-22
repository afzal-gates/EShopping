namespace Shipping.Application.Responses;

public class ShipmentResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string TrackingNumber { get; set; }
    public string CarrierCode { get; set; }
    public string CarrierName { get; set; }
    public string ServiceType { get; set; }
    public string Status { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? EstimatedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal Weight { get; set; }
    public string ToAddressLine1 { get; set; }
    public string ToCity { get; set; }
    public string ToState { get; set; }
    public string ToZipCode { get; set; }
    public string? LabelUrl { get; set; }
    public DateTime CreatedDate { get; set; }
}
