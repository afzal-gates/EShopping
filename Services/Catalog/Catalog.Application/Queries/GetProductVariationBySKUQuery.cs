using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductVariationBySKUQuery : IRequest<ProductVariationResponse>
{
    public string SKU { get; set; }

    public GetProductVariationBySKUQuery(string sku)
    {
        SKU = sku;
    }
}
