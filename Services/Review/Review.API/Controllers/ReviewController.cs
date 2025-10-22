using Review.Application.Commands;
using Review.Application.Queries;
using Review.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Review.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ReviewController> _logger;

    public ReviewController(IMediator mediator, ILogger<ReviewController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all reviews for a product
    /// </summary>
    [HttpGet("product/{productId}", Name = "GetProductReviews")]
    [ProducesResponseType(typeof(List<ProductReviewResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<ProductReviewResponse>>> GetProductReviews(
        string productId,
        [FromQuery] string? variationId,
        [FromQuery] int? minRating,
        [FromQuery] int? maxRating,
        [FromQuery] bool? verifiedPurchaseOnly,
        [FromQuery] string orderBy = "MostRecent",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = new GetProductReviewsQuery
        {
            ProductId = productId,
            VariationId = variationId,
            MinRating = minRating,
            MaxRating = maxRating,
            VerifiedPurchaseOnly = verifiedPurchaseOnly,
            OrderBy = orderBy,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Create a new product review
    /// </summary>
    [HttpPost(Name = "CreateReview")]
    [ProducesResponseType(typeof(ProductReviewResponse), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ProductReviewResponse>> CreateReview([FromBody] CreateReviewCommand command)
    {
        var result = await _mediator.Send(command);
        return Created($"/api/v1/review/{result.Id}", result);
    }

    /// <summary>
    /// Moderate a review (approve/reject)
    /// </summary>
    [HttpPut("{reviewId}/moderate", Name = "ModerateReview")]
    [ProducesResponseType(typeof(ProductReviewResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductReviewResponse>> ModerateReview(
        string reviewId,
        [FromBody] ModerateReviewCommand command)
    {
        command.ReviewId = reviewId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Vote on review helpfulness
    /// </summary>
    [HttpPost("{reviewId}/vote", Name = "VoteReview")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> VoteReview(
        string reviewId,
        [FromBody] VoteReviewCommand command)
    {
        command.ReviewId = reviewId;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Get review statistics for a product
    /// </summary>
    [HttpGet("product/{productId}/stats", Name = "GetReviewStats")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<object>> GetReviewStats(string productId)
    {
        // This would aggregate reviews to show rating distribution
        var stats = new
        {
            ProductId = productId,
            AverageRating = 4.5m,
            TotalReviews = 150,
            RatingDistribution = new
            {
                FiveStar = 80,
                FourStar = 45,
                ThreeStar = 15,
                TwoStar = 7,
                OneStar = 3
            },
            VerifiedPurchasePercentage = 75
        };

        return Ok(stats);
    }
}
