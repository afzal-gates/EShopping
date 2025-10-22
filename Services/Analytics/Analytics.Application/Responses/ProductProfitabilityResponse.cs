namespace Analytics.Application.Responses;

public class ProductProfitabilityResponse
{
    public int Id { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string? SKU { get; set; }

    public int TotalUnitsSold { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageSellingPrice { get; set; }

    public decimal TotalCOGS { get; set; }
    public decimal AverageCostPerUnit { get; set; }

    public decimal GrossProfit { get; set; }
    public decimal GrossProfitMargin { get; set; }
    public decimal NetProfit { get; set; }
    public decimal NetProfitMargin { get; set; }

    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string PeriodType { get; set; }

    public int ReturnCount { get; set; }
    public decimal ReturnRate { get; set; }
    public decimal DiscountAmount { get; set; }
}
