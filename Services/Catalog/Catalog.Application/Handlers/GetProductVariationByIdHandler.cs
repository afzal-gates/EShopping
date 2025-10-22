using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductVariationByIdHandler : IRequestHandler<GetProductVariationByIdQuery, ProductVariationResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductVariationByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductVariationResponse> Handle(GetProductVariationByIdQuery request, CancellationToken cancellationToken)
    {
        var variation = await _productRepository.GetProductVariationById(request.ProductId, request.VariationId);
        var variationResponse = ProductMapper.Mapper.Map<ProductVariationResponse>(variation);
        return variationResponse;
    }
}
