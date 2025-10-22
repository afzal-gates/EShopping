namespace WishList.Application.Responses;

public class WishListResponse
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public bool IsPublic { get; set; }
    public string? ShareToken { get; set; }
    public List<WishListItemResponse> Items { get; set; } = new List<WishListItemResponse>();
    public DateTime CreatedDate { get; set; }
    public int TotalItems => Items?.Count ?? 0;
    public decimal TotalValue => Items?.Sum(i => i.Price * i.Quantity) ?? 0;
}

public class WishListItemResponse
{
    public string ItemId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string? VariationId { get; set; }
    public string? SKU { get; set; }
    public decimal Price { get; set; }
    public decimal OriginalPrice { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; }
    public int Priority { get; set; }
    public string? Notes { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime AddedDate { get; set; }
    public bool PriceAlertEnabled { get; set; }
    public decimal? TargetPrice { get; set; }
    public bool AlertSent { get; set; }
    public bool IsPriceDropped => TargetPrice.HasValue && Price <= TargetPrice.Value;
}
