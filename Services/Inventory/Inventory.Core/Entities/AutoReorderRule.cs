namespace Inventory.Core.Entities;

/// <summary>
/// Rules for automated reorder point calculations and PO generation
/// </summary>
public class AutoReorderRule : EntityBase
{
    public int InventoryItemId { get; set; }
    public string ProductId { get; set; }
    public string? VariationId { get; set; }
    public string SKU { get; set; }

    // Reorder settings
    public bool IsEnabled { get; set; } = true;
    public int ReorderPoint { get; set; }
    public int ReorderQuantity { get; set; }
    public int SafetyStockLevel { get; set; }
    public int MaxStockLevel { get; set; }

    // Supplier settings
    public int PreferredSupplierId { get; set; }
    public string PreferredSupplierName { get; set; }
    public int LeadTimeDays { get; set; }
    public decimal UnitCost { get; set; }
    public int MinimumOrderQuantity { get; set; } = 1;

    // Auto-ordering behavior
    public bool AutoCreatePO { get; set; } = false;
    public bool RequiresApproval { get; set; } = true;
    public decimal MaxAutoOrderValue { get; set; } = 1000; // Auto-approve up to this amount

    // Analysis settings
    public bool UseDemandForecast { get; set; } = false;
    public int DemandForecastDays { get; set; } = 30;
    public decimal AverageDailyDemand { get; set; }

    // Notification settings
    public bool SendEmailAlert { get; set; } = true;
    public bool SendSlackAlert { get; set; } = false;
    public string? AlertEmail { get; set; }
    public string? SlackChannel { get; set; }

    // Scheduling
    public DateTime? LastCheckDate { get; set; }
    public DateTime? LastReorderDate { get; set; }
    public int CheckFrequencyHours { get; set; } = 24;

    public string? Notes { get; set; }

    // Navigation
    public InventoryItem InventoryItem { get; set; }
}
