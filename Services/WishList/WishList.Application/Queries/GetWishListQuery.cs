using WishList.Application.Responses;
using MediatR;

namespace WishList.Application.Queries;

public class GetWishListQuery : IRequest<WishListResponse>
{
    public string UserId { get; set; }
}
