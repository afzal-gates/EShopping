namespace Inventory.Core.Entities;

/// <summary>
/// Warehouse/fulfillment center information
/// </summary>
public class Warehouse : EntityBase
{
    public string WarehouseCode { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Location
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }

    // Contact
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }

    // Capacity
    public int TotalCapacity { get; set; }
    public int UsedCapacity { get; set; }
    public int AvailableCapacity => TotalCapacity - UsedCapacity;
    public string? CapacityUnit { get; set; } = "pallets";

    // Status
    public bool IsActive { get; set; } = true;
    public bool IsPrimary { get; set; } = false;
    public int Priority { get; set; } = 1; // For fulfillment routing

    // Operating hours
    public string? OperatingHours { get; set; }
    public string? TimeZone { get; set; }

    public string? Notes { get; set; }

    // Navigation
    public ICollection<WarehouseInventory> Inventory { get; set; } = new List<WarehouseInventory>();
}
