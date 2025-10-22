using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductVariationHandler : IRequestHandler<UpdateProductVariationCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductVariationHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductVariationCommand request, CancellationToken cancellationToken)
    {
        var variationEntity = ProductMapper.Mapper.Map<ProductVariation>(request);
        variationEntity.Id = request.VariationId;

        return await _productRepository.UpdateProductVariation(request.ProductId, variationEntity);
    }
}
