using Review.Application.Responses;
using MediatR;

namespace Review.Application.Commands;

public class ModerateReviewCommand : IRequest<ProductReviewResponse>
{
    public string ReviewId { get; set; }
    public string Status { get; set; } // Approved, Rejected
    public string ModeratedBy { get; set; }
    public string? RejectionReason { get; set; }
}
