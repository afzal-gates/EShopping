using Search.Application.Commands;
using Search.Application.Queries;
using Search.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Search.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<SearchController> _logger;

    public SearchController(IMediator mediator, ILogger<SearchController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Search products with faceted filtering
    /// </summary>
    [HttpGet("products", Name = "SearchProducts")]
    [ProducesResponseType(typeof(SearchResultResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<SearchResultResponse>> SearchProducts(
        [FromQuery] string? query,
        [FromQuery] string? category,
        [FromQuery] string? brand,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] double? minRating,
        [FromQuery] bool? isAvailable,
        [FromQuery] bool? isFeatured,
        [FromQuery] string sortBy = "Relevance",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
    {
        var searchQuery = new SearchProductsQuery
        {
            Query = query,
            Category = category,
            Brand = brand,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            MinRating = minRating,
            IsAvailable = isAvailable,
            IsFeatured = isFeatured,
            SortBy = sortBy,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(searchQuery);
        return Ok(result);
    }

    /// <summary>
    /// Get autocomplete suggestions
    /// </summary>
    [HttpGet("autocomplete", Name = "GetAutoComplete")]
    [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<string>>> GetAutoComplete([FromQuery] string query)
    {
        // This would query Elasticsearch completion suggester
        var suggestions = new List<string>
        {
            query + " shirt",
            query + " shoes",
            query + " jacket"
        };

        return Ok(suggestions);
    }

    /// <summary>
    /// Index a product (called by Catalog service via event)
    /// </summary>
    [HttpPost("index/product", Name = "IndexProduct")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> IndexProduct([FromBody] IndexProductCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Reindex all products
    /// </summary>
    [HttpPost("reindex", Name = "ReindexAllProducts")]
    [ProducesResponseType(typeof(object), (int)HttpStatusCode.Accepted)]
    public async Task<ActionResult<object>> ReindexAllProducts()
    {
        // This would trigger a background job to reindex all products
        return Accepted(new { Message = "Reindex job started", Status = "Processing" });
    }
}
