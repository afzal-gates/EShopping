using MediatR;

namespace Catalog.Application.Commands;

public class DeleteProductVariationCommand : IRequest<bool>
{
    public string ProductId { get; set; }
    public string VariationId { get; set; }
}
