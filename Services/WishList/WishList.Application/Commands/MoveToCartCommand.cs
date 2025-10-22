using MediatR;

namespace WishList.Application.Commands;

public class MoveToCartCommand : IRequest<bool>
{
    public string UserId { get; set; }
    public string WishListItemId { get; set; }
}
