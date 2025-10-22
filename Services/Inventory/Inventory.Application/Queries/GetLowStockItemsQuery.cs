using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries;

public class GetLowStockItemsQuery : IRequest<IList<InventoryItemResponse>>
{
}
