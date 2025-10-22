using Analytics.Application.Responses;
using MediatR;

namespace Analytics.Application.Queries;

public class GetProductProfitabilityQuery : IRequest<List<ProductProfitabilityResponse>>
{
    public string? ProductId { get; set; }
    public string? SKU { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string PeriodType { get; set; } = "Monthly";
    public int Top { get; set; } = 100;
    public string OrderBy { get; set; } = "GrossProfit"; // GrossProfit, NetProfit, Revenue, Margin
    public bool Descending { get; set; } = true;
}
