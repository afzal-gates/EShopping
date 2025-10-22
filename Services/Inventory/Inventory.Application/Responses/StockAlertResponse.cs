namespace Inventory.Application.Responses;

public class StockAlertResponse
{
    public int Id { get; set; }
    public int InventoryItemId { get; set; }
    public string SKU { get; set; }
    public string ProductName { get; set; }
    public string Status { get; set; }
    public int CurrentQuantity { get; set; }
    public int ThresholdQuantity { get; set; }
    public string AlertType { get; set; }
    public string? NotificationSentTo { get; set; }
    public DateTime? NotificationSentDate { get; set; }
    public DateTime? ResolvedDate { get; set; }
    public string? ResolvedBy { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedDate { get; set; }
}
