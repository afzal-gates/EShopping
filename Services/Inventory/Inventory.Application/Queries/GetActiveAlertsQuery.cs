using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries;

public class GetActiveAlertsQuery : IRequest<IList<StockAlertResponse>>
{
}
