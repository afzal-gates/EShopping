using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Application.Responses;

public class ProductVariationResponse
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string SKU { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public string? Material { get; set; }
    public int StockQuantity { get; set; }
    public decimal PriceAdjustment { get; set; }
    public string? ImageFile { get; set; }
    public bool IsActive { get; set; }
    public string ProductId { get; set; }
}
