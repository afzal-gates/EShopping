using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class AddProductVariationHandler : IRequestHandler<AddProductVariationCommand, ProductVariationResponse>
{
    private readonly IProductRepository _productRepository;

    public AddProductVariationHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductVariationResponse> Handle(AddProductVariationCommand request, CancellationToken cancellationToken)
    {
        var variationEntity = ProductMapper.Mapper.Map<ProductVariation>(request);
        if (variationEntity is null)
        {
            throw new ApplicationException("There is an issue with mapping while creating new product variation");
        }

        var newVariation = await _productRepository.AddProductVariation(request.ProductId, variationEntity);
        if (newVariation == null)
        {
            throw new ApplicationException("Failed to add product variation");
        }

        var variationResponse = ProductMapper.Mapper.Map<ProductVariationResponse>(newVariation);
        return variationResponse;
    }
}
