using Inventory.Application.Responses;
using MediatR;

namespace Inventory.Application.Commands;

public class CreateAutoReorderRuleCommand : IRequest<AutoReorderRuleResponse>
{
    public int InventoryItemId { get; set; }
    public bool IsEnabled { get; set; } = true;
    public int ReorderPoint { get; set; }
    public int ReorderQuantity { get; set; }
    public int SafetyStockLevel { get; set; }
    public int MaxStockLevel { get; set; }
    public int PreferredSupplierId { get; set; }
    public int LeadTimeDays { get; set; }
    public decimal UnitCost { get; set; }
    public int MinimumOrderQuantity { get; set; } = 1;
    public bool AutoCreatePO { get; set; } = false;
    public bool RequiresApproval { get; set; } = true;
    public decimal MaxAutoOrderValue { get; set; } = 1000;
    public bool SendEmailAlert { get; set; } = true;
    public string? AlertEmail { get; set; }
    public string? Notes { get; set; }
}
