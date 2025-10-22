using Catalog.Application.Commands;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class DeleteProductVariationHandler : IRequestHandler<DeleteProductVariationCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductVariationHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductVariationCommand request, CancellationToken cancellationToken)
    {
        return await _productRepository.DeleteProductVariation(request.ProductId, request.VariationId);
    }
}
