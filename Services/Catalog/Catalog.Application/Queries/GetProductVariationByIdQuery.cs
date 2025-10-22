using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductVariationByIdQuery : IRequest<ProductVariationResponse>
{
    public string ProductId { get; set; }
    public string VariationId { get; set; }

    public GetProductVariationByIdQuery(string productId, string variationId)
    {
        ProductId = productId;
        VariationId = variationId;
    }
}
