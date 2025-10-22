namespace Analytics.Application.Responses;

public class OrderFinancialsResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string OrderNumber { get; set; }
    public string UserName { get; set; }
    public DateTime OrderDate { get; set; }

    public decimal SubTotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalRevenue { get; set; }

    public decimal ProductCOGS { get; set; }
    public decimal ShippingCOGS { get; set; }
    public decimal PaymentProcessingFee { get; set; }
    public decimal TotalCOGS { get; set; }

    public decimal DiscountAmount { get; set; }
    public string? DiscountCode { get; set; }

    public decimal GrossProfit { get; set; }
    public decimal GrossProfitMargin { get; set; }
    public decimal NetProfit { get; set; }
    public decimal NetProfitMargin { get; set; }

    public string OrderStatus { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsRefunded { get; set; }
    public decimal RefundAmount { get; set; }
}
