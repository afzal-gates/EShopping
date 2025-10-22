namespace Ordering.Application.Responses;

public class OrderStatusHistoryResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string FromStatus { get; set; }
    public string ToStatus { get; set; }
    public DateTime StatusChangeDate { get; set; }
    public string? ChangedBy { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
}
