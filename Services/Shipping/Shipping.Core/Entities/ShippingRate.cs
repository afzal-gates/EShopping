using Shipping.Core.Enums;

namespace Shipping.Core.Entities;

public class ShippingRate : EntityBase
{
    public CarrierCode CarrierCode { get; set; }
    public string ServiceType { get; set; }
    public decimal Cost { get; set; }
    public int EstimatedDays { get; set; }
    public string CacheKey { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string FromZipCode { get; set; }
    public string ToZipCode { get; set; }
    public decimal Weight { get; set; }
}
