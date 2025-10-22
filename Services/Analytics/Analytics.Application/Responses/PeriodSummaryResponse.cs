namespace Analytics.Application.Responses;

public class PeriodSummaryResponse
{
    public int Id { get; set; }
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public string PeriodType { get; set; }

    public int TotalOrders { get; set; }
    public int CompletedOrders { get; set; }
    public int CancelledOrders { get; set; }
    public int RefundedOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageOrderValue { get; set; }

    public decimal TotalCOGS { get; set; }
    public decimal TotalShippingCosts { get; set; }
    public decimal TotalPaymentFees { get; set; }
    public decimal TotalOperatingExpenses { get; set; }

    public decimal GrossProfit { get; set; }
    public decimal GrossProfitMargin { get; set; }
    public decimal NetProfit { get; set; }
    public decimal NetProfitMargin { get; set; }

    public decimal TotalDiscounts { get; set; }
    public decimal TotalRefunds { get; set; }

    public int UniqueCustomers { get; set; }
    public int NewCustomers { get; set; }
    public int ReturningCustomers { get; set; }

    public int TotalProductsSold { get; set; }
    public string? TopSellingProductId { get; set; }
    public string? TopSellingProductName { get; set; }
    public int TopSellingProductQuantity { get; set; }
}
