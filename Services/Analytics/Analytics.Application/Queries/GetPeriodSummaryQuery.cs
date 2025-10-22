using Analytics.Application.Responses;
using MediatR;

namespace Analytics.Application.Queries;

public class GetPeriodSummaryQuery : IRequest<List<PeriodSummaryResponse>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string PeriodType { get; set; } = "Monthly"; // Daily, Weekly, Monthly, Quarterly, Yearly
    public int Top { get; set; } = 12;
}
