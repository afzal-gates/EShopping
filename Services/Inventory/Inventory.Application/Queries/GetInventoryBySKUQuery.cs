using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries;

public class GetInventoryBySKUQuery : IRequest<InventoryItemResponse>
{
    public string SKU { get; set; }

    public GetInventoryBySKUQuery(string sku)
    {
        SKU = sku;
    }
}
