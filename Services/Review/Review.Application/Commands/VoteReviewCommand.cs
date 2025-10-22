using MediatR;

namespace Review.Application.Commands;

public class VoteReviewCommand : IRequest<bool>
{
    public string ReviewId { get; set; }
    public string UserId { get; set; }
    public bool IsHelpful { get; set; }
}
