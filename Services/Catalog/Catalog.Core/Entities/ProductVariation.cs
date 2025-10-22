using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductVariation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("SKU")]
    public string SKU { get; set; }

    [BsonElement("Size")]
    public string? Size { get; set; }

    [BsonElement("Color")]
    public string? Color { get; set; }

    [BsonElement("Material")]
    public string? Material { get; set; }

    [BsonElement("StockQuantity")]
    public int StockQuantity { get; set; }

    [BsonRepresentation(BsonType.Decimal128)]
    [BsonElement("PriceAdjustment")]
    public decimal PriceAdjustment { get; set; } = 0;

    [BsonElement("ImageFile")]
    public string? ImageFile { get; set; }

    [BsonElement("IsActive")]
    public bool IsActive { get; set; } = true;

    [BsonElement("ProductId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; }
}
