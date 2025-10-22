using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WishList.Core.Entities;

/// <summary>
/// User wish list with items and price alerts
/// </summary>
public class WishList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("UserId")]
    public string UserId { get; set; }

    [BsonElement("UserName")]
    public string UserName { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; } = "My Wish List";

    [BsonElement("IsPublic")]
    public bool IsPublic { get; set; } = false;

    [BsonElement("ShareToken")]
    public string? ShareToken { get; set; }

    [BsonElement("Items")]
    public List<WishListItem> Items { get; set; } = new List<WishListItem>();

    [BsonElement("CreatedDate")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [BsonElement("LastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }
}

/// <summary>
/// Individual wish list item
/// </summary>
public class WishListItem
{
    [BsonElement("ItemId")]
    public string ItemId { get; set; }

    [BsonElement("ProductId")]
    public string ProductId { get; set; }

    [BsonElement("ProductName")]
    public string ProductName { get; set; }

    [BsonElement("VariationId")]
    public string? VariationId { get; set; }

    [BsonElement("SKU")]
    public string? SKU { get; set; }

    [BsonElement("Price")]
    public decimal Price { get; set; }

    [BsonElement("OriginalPrice")]
    public decimal OriginalPrice { get; set; }

    [BsonElement("ImageUrl")]
    public string? ImageUrl { get; set; }

    [BsonElement("Quantity")]
    public int Quantity { get; set; } = 1;

    [BsonElement("Priority")]
    public int Priority { get; set; } = 3; // 1 = High, 3 = Medium, 5 = Low

    [BsonElement("Notes")]
    public string? Notes { get; set; }

    [BsonElement("IsAvailable")]
    public bool IsAvailable { get; set; } = true;

    [BsonElement("AddedDate")]
    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    // Price alert
    [BsonElement("PriceAlertEnabled")]
    public bool PriceAlertEnabled { get; set; } = false;

    [BsonElement("TargetPrice")]
    public decimal? TargetPrice { get; set; }

    [BsonElement("AlertSent")]
    public bool AlertSent { get; set; } = false;

    [BsonElement("AlertSentDate")]
    public DateTime? AlertSentDate { get; set; }
}
