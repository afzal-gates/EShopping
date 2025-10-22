using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductVariationsHandler : IRequestHandler<GetProductVariationsQuery, IList<ProductVariationResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductVariationsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IList<ProductVariationResponse>> Handle(GetProductVariationsQuery request, CancellationToken cancellationToken)
    {
        var variations = await _productRepository.GetProductVariations(request.ProductId);
        var variationResponses = ProductMapper.Mapper.Map<IList<ProductVariationResponse>>(variations);
        return variationResponses;
    }
}
