using Ordering.Application.Responses;
using MediatR;

namespace Ordering.Application.Queries;

public class GetOrderStatusHistoryQuery : IRequest<IList<OrderStatusHistoryResponse>>
{
    public int OrderId { get; set; }

    public GetOrderStatusHistoryQuery(int orderId)
    {
        OrderId = orderId;
    }
}
