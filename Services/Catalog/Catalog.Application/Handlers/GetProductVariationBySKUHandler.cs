using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductVariationBySKUHandler : IRequestHandler<GetProductVariationBySKUQuery, ProductVariationResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductVariationBySKUHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductVariationResponse> Handle(GetProductVariationBySKUQuery request, CancellationToken cancellationToken)
    {
        var variation = await _productRepository.GetProductVariationBySKU(request.SKU);
        var variationResponse = ProductMapper.Mapper.Map<ProductVariationResponse>(variation);
        return variationResponse;
    }
}
