namespace Inventory.Application.Responses;

public class WarehouseResponse
{
    public int Id { get; set; }
    public string WarehouseCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string AddressLine1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
    public int TotalCapacity { get; set; }
    public int UsedCapacity { get; set; }
    public int AvailableCapacity { get; set; }
    public bool IsActive { get; set; }
    public bool IsPrimary { get; set; }
    public int Priority { get; set; }
}
