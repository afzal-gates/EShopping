using Search.Application.Responses;
using MediatR;

namespace Search.Application.Queries;

public class SearchProductsQuery : IRequest<SearchResultResponse>
{
    // Search term
    public string? Query { get; set; }

    // Filters
    public string? Category { get; set; }
    public string? Brand { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public double? MinRating { get; set; }
    public List<string>? Sizes { get; set; }
    public List<string>? Colors { get; set; }
    public List<string>? Materials { get; set; }
    public bool? IsAvailable { get; set; }
    public bool? IsFeatured { get; set; }

    // Sorting
    public string SortBy { get; set; } = "Relevance"; // Relevance, PriceLowToHigh, PriceHighToLow, Rating, Popularity, Newest

    // Pagination
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    // Suggestions
    public bool IncludeAutoComplete { get; set; } = false;
}
