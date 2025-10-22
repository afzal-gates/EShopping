using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Commands;

public class CreateWarehouseCommand : IRequest<WarehouseResponse>
{
    public string WarehouseCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public int TotalCapacity { get; set; }
    public string? CapacityUnit { get; set; } = "pallets";
    public bool IsActive { get; set; } = true;
    public bool IsPrimary { get; set; } = false;
    public int Priority { get; set; } = 1;
    public string? Notes { get; set; }
}
