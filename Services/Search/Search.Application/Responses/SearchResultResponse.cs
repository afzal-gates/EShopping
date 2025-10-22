namespace Search.Application.Responses;

public class SearchResultResponse
{
    public string Query { get; set; }
    public int TotalResults { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<ProductSearchResult> Results { get; set; } = new List<ProductSearchResult>();
    public SearchFacets Facets { get; set; }
    public List<string> Suggestions { get; set; } = new List<string>();
    public double SearchTime { get; set; } // milliseconds
}

public class ProductSearchResult
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public string ImageFile { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsFeatured { get; set; }
    public double SearchScore { get; set; }
}

public class SearchFacets
{
    public List<FacetValue> Categories { get; set; } = new List<FacetValue>();
    public List<FacetValue> Brands { get; set; } = new List<FacetValue>();
    public List<FacetValue> Sizes { get; set; } = new List<FacetValue>();
    public List<FacetValue> Colors { get; set; } = new List<FacetValue>();
    public List<FacetValue> Materials { get; set; } = new List<FacetValue>();
    public PriceRange PriceRange { get; set; }
    public RatingDistribution RatingDistribution { get; set; }
}

public class FacetValue
{
    public string Value { get; set; }
    public int Count { get; set; }
}

public class PriceRange
{
    public decimal Min { get; set; }
    public decimal Max { get; set; }
}

public class RatingDistribution
{
    public int FiveStar { get; set; }
    public int FourStar { get; set; }
    public int ThreeStar { get; set; }
    public int TwoStar { get; set; }
    public int OneStar { get; set; }
}
