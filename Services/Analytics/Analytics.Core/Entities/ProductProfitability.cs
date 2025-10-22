namespace Analytics.Core.Entities;

/// <summary>
/// Tracks profitability metrics for each product/SKU
/// </summary>
public class ProductProfitability : EntityBase
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string? SKU { get; set; }

    // Revenue metrics
    public int TotalUnitsSold { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageSellingPrice { get; set; }

    // Cost metrics
    public decimal TotalCOGS { get; set; } // Cost of Goods Sold
    public decimal AverageCostPerUnit { get; set; }

    // Profitability
    public decimal GrossProfit { get; set; }
    public decimal GrossProfitMargin { get; set; } // Percentage
    public decimal NetProfit { get; set; }
    public decimal NetProfitMargin { get; set; }

    // Period tracking
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string PeriodType { get; set; } // Daily, Weekly, Monthly, Quarterly, Yearly

    // Additional metrics
    public int ReturnCount { get; set; }
    public decimal ReturnRate { get; set; }
    public decimal DiscountAmount { get; set; }
}
