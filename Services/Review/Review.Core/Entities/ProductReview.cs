using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review.Core.Entities;

/// <summary>
/// Customer product reviews with ratings and moderation
/// </summary>
public class ProductReview
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("ProductId")]
    public string ProductId { get; set; }

    [BsonElement("ProductName")]
    public string ProductName { get; set; }

    [BsonElement("VariationId")]
    public string? VariationId { get; set; }

    [BsonElement("SKU")]
    public string? SKU { get; set; }

    // Reviewer details
    [BsonElement("UserId")]
    public string UserId { get; set; }

    [BsonElement("UserName")]
    public string UserName { get; set; }

    [BsonElement("UserEmail")]
    public string UserEmail { get; set; }

    // Review content
    [BsonElement("Rating")]
    public int Rating { get; set; } // 1-5 stars

    [BsonElement("Title")]
    public string Title { get; set; }

    [BsonElement("ReviewText")]
    public string ReviewText { get; set; }

    [BsonElement("Pros")]
    public string? Pros { get; set; }

    [BsonElement("Cons")]
    public string? Cons { get; set; }

    // Purchase verification
    [BsonElement("IsVerifiedPurchase")]
    public bool IsVerifiedPurchase { get; set; }

    [BsonElement("OrderId")]
    public int? OrderId { get; set; }

    [BsonElement("PurchaseDate")]
    public DateTime? PurchaseDate { get; set; }

    // Images
    [BsonElement("Images")]
    public List<ReviewImage> Images { get; set; } = new List<ReviewImage>();

    // Moderation
    [BsonElement("Status")]
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

    [BsonElement("IsVisible")]
    public bool IsVisible { get; set; } = false;

    [BsonElement("ModeratedBy")]
    public string? ModeratedBy { get; set; }

    [BsonElement("ModeratedDate")]
    public DateTime? ModeratedDate { get; set; }

    [BsonElement("RejectionReason")]
    public string? RejectionReason { get; set; }

    // Voting
    [BsonElement("HelpfulCount")]
    public int HelpfulCount { get; set; } = 0;

    [BsonElement("NotHelpfulCount")]
    public int NotHelpfulCount { get; set; } = 0;

    [BsonElement("Votes")]
    public List<ReviewVote> Votes { get; set; } = new List<ReviewVote>();

    // Metadata
    [BsonElement("ReviewDate")]
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow;

    [BsonElement("LastModifiedDate")]
    public DateTime? LastModifiedDate { get; set; }

    [BsonElement("ReportCount")]
    public int ReportCount { get; set; } = 0;
}

/// <summary>
/// Review image attachment
/// </summary>
public class ReviewImage
{
    [BsonElement("ImageUrl")]
    public string ImageUrl { get; set; }

    [BsonElement("Caption")]
    public string? Caption { get; set; }

    [BsonElement("UploadDate")]
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// User votes on review helpfulness
/// </summary>
public class ReviewVote
{
    [BsonElement("UserId")]
    public string UserId { get; set; }

    [BsonElement("IsHelpful")]
    public bool IsHelpful { get; set; }

    [BsonElement("VoteDate")]
    public DateTime VoteDate { get; set; } = DateTime.UtcNow;
}
