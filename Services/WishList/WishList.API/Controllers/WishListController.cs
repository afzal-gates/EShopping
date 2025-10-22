using WishList.Application.Commands;
using WishList.Application.Queries;
using WishList.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WishList.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class WishListController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WishListController> _logger;

    public WishListController(IMediator mediator, ILogger<WishListController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get user's wish list
    /// </summary>
    [HttpGet("{userId}", Name = "GetWishList")]
    [ProducesResponseType(typeof(WishListResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<WishListResponse>> GetWishList(string userId)
    {
        var query = new GetWishListQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Add item to wish list
    /// </summary>
    [HttpPost("items", Name = "AddToWishList")]
    [ProducesResponseType(typeof(WishListResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<WishListResponse>> AddToWishList([FromBody] AddToWishListCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Move item from wish list to cart
    /// </summary>
    [HttpPost("items/move-to-cart", Name = "MoveToCart")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> MoveToCart([FromBody] MoveToCartCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Remove item from wish list
    /// </summary>
    [HttpDelete("{userId}/items/{itemId}", Name = "RemoveFromWishList")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> RemoveFromWishList(string userId, string itemId)
    {
        // Remove item command would be here
        return NoContent();
    }

    /// <summary>
    /// Share wish list
    /// </summary>
    [HttpPost("{userId}/share", Name = "ShareWishList")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<object>> ShareWishList(string userId)
    {
        var shareToken = Guid.NewGuid().ToString("N");
        var shareUrl = $"{Request.Scheme}://{Request.Host}/shared/wishlist/{shareToken}";

        return Ok(new
        {
            ShareToken = shareToken,
            ShareUrl = shareUrl,
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        });
    }

    /// <summary>
    /// Get price alerts for user
    /// </summary>
    [HttpGet("{userId}/price-alerts", Name = "GetPriceAlerts")]
    [ProducesResponseType(typeof(List<WishListItemResponse>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<WishListItemResponse>>> GetPriceAlerts(string userId)
    {
        // Query items with price alerts enabled
        return Ok(new List<WishListItemResponse>());
    }
}
