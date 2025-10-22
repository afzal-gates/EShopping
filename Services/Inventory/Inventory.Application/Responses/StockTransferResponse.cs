namespace Inventory.Application.Responses;

public class StockTransferResponse
{
    public int Id { get; set; }
    public string TransferNumber { get; set; }
    public int FromWarehouseId { get; set; }
    public string FromWarehouseName { get; set; }
    public int ToWarehouseId { get; set; }
    public string ToWarehouseName { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string SKU { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? ExpectedArrivalDate { get; set; }
    public DateTime? ActualArrivalDate { get; set; }
    public string? TrackingNumber { get; set; }
    public string RequestedBy { get; set; }
    public string Reason { get; set; }
}
