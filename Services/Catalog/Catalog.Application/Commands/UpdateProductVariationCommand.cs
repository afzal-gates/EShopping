using MediatR;

namespace Catalog.Application.Commands;

public class UpdateProductVariationCommand : IRequest<bool>
{
    public string ProductId { get; set; }
    public string VariationId { get; set; }
    public string SKU { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public string? Material { get; set; }
    public int StockQuantity { get; set; }
    public decimal PriceAdjustment { get; set; }
    public string? ImageFile { get; set; }
    public bool IsActive { get; set; }
}
