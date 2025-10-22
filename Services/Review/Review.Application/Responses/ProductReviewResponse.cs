namespace Review.Application.Responses;

public class ProductReviewResponse
{
    public string Id { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string? SKU { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; }
    public string ReviewText { get; set; }
    public string? Pros { get; set; }
    public string? Cons { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public List<string> ImageUrls { get; set; }
    public string Status { get; set; }
    public bool IsVisible { get; set; }
    public int HelpfulCount { get; set; }
    public int NotHelpfulCount { get; set; }
    public DateTime ReviewDate { get; set; }
}
