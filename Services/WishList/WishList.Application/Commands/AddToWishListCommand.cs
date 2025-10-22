using WishList.Application.Responses;
using MediatR;

namespace WishList.Application.Commands;

public class AddToWishListCommand : IRequest<WishListResponse>
{
    public string UserId { get; set; }
    public string ProductId { get; set; }
    public string? VariationId { get; set; }
    public string? SKU { get; set; }
    public int Quantity { get; set; } = 1;
    public int Priority { get; set; } = 3;
    public bool EnablePriceAlert { get; set; } = false;
    public decimal? TargetPrice { get; set; }
    public string? Notes { get; set; }
}
