using Review.Application.Responses;
using MediatR;

namespace Review.Application.Commands;

public class CreateReviewCommand : IRequest<ProductReviewResponse>
{
    public string ProductId { get; set; }
    public string? VariationId { get; set; }
    public string? SKU { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    public int Rating { get; set; }
    public string Title { get; set; }
    public string ReviewText { get; set; }
    public string? Pros { get; set; }
    public string? Cons { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public int? OrderId { get; set; }
    public List<string>? ImageUrls { get; set; }
}
