namespace Analytics.Core.Entities;

/// <summary>
/// Aggregated financial summary for a time period
/// </summary>
public class PeriodSummary : EntityBase
{
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string PeriodType { get; set; } // Daily, Weekly, Monthly, Quarterly, Yearly

    // Sales metrics
    public int TotalOrders { get; set; }
    public int CompletedOrders { get; set; }
    public int CancelledOrders { get; set; }
    public int RefundedOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageOrderValue { get; set; }

    // Cost metrics
    public decimal TotalCOGS { get; set; }
    public decimal TotalShippingCosts { get; set; }
    public decimal TotalPaymentFees { get; set; }
    public decimal TotalOperatingExpenses { get; set; }

    // Profitability
    public decimal GrossProfit { get; set; }
    public decimal GrossProfitMargin { get; set; }
    public decimal NetProfit { get; set; }
    public decimal NetProfitMargin { get; set; }

    // Discounts and returns
    public decimal TotalDiscounts { get; set; }
    public decimal TotalRefunds { get; set; }

    // Customer metrics
    public int UniqueCustomers { get; set; }
    public int NewCustomers { get; set; }
    public int ReturningCustomers { get; set; }

    // Product metrics
    public int TotalProductsSold { get; set; }
    public string? TopSellingProductId { get; set; }
    public string? TopSellingProductName { get; set; }
    public int TopSellingProductQuantity { get; set; }
}
