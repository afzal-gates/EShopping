using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductVariationsQuery : IRequest<IList<ProductVariationResponse>>
{
    public string ProductId { get; set; }

    public GetProductVariationsQuery(string productId)
    {
        ProductId = productId;
    }
}
