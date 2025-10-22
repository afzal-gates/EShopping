using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Queries;

public class GetInventoryItemQuery : IRequest<InventoryItemResponse>
{
    public int Id { get; set; }

    public GetInventoryItemQuery(int id)
    {
        Id = id;
    }
}
