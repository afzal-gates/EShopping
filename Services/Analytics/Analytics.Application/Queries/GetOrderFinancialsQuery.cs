using Analytics.Application.Responses;
using MediatR;

namespace Analytics.Application.Queries;

public class GetOrderFinancialsQuery : IRequest<List<OrderFinancialsResponse>>
{
    public int? OrderId { get; set; }
    public string? UserName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? OrderStatus { get; set; }
    public bool IncludeCancelled { get; set; } = false;
    public bool IncludeRefunded { get; set; } = true;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
