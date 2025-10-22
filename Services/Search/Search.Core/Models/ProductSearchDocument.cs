namespace Search.Core.Models;

/// <summary>
/// Elasticsearch document for product search indexing
/// </summary>
public class ProductSearchDocument
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    public string ImageFile { get; set; }

    // Search relevance
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public int SalesCount { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsFeatured { get; set; }

    // Variations for faceted search
    public List<ProductVariationSearchDocument> Variations { get; set; } = new List<ProductVariationSearchDocument>();

    // Facets
    public List<string> AvailableSizes { get; set; } = new List<string>();
    public List<string> AvailableColors { get; set; } = new List<string>();
    public List<string> AvailableMaterials { get; set; } = new List<string>();
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }

    // Metadata
    public DateTime IndexedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Product variation data in search index
/// </summary>
public class ProductVariationSearchDocument
{
    public string VariationId { get; set; }
    public string SKU { get; set; }
    public string? Size { get; set; }
    public string? Color { get; set; }
    public string? Material { get; set; }
    public decimal PriceAdjustment { get; set; }
    public int StockQuantity { get; set; }
    public bool IsAvailable { get; set; }
}
