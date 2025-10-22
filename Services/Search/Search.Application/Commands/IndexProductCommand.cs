using MediatR;

namespace Search.Application.Commands;

public class IndexProductCommand : IRequest<bool>
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Summary { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public List<string> Tags { get; set; }
    public string ImageFile { get; set; }
    public double AverageRating { get; set; }
    public int ReviewCount { get; set; }
    public bool IsAvailable { get; set; }
}
