using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries;

public class GetStockMovementHistoryQuery : IRequest<IList<StockMovementResponse>>
{
    public string SKU { get; set; }

    public GetStockMovementHistoryQuery(string sku)
    {
        SKU = sku;
    }
}
