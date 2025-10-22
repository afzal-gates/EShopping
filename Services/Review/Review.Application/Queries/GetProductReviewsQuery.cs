using Review.Application.Responses;
using MediatR;

namespace Review.Application.Queries;

public class GetProductReviewsQuery : IRequest<List<ProductReviewResponse>>
{
    public string ProductId { get; set; }
    public string? VariationId { get; set; }
    public int? MinRating { get; set; }
    public int? MaxRating { get; set; }
    public bool? VerifiedPurchaseOnly { get; set; }
    public string OrderBy { get; set; } = "MostRecent"; // MostRecent, MostHelpful, HighestRated, LowestRated
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
