using Inventory.Core.Entities;
using Inventory.Core.Enums;

namespace Inventory.Core.Repositories;

public interface IStockAlertRepository : IAsyncRepository<StockAlert>
{
    Task<IReadOnlyList<StockAlert>> GetActiveAlertsAsync();
    Task<IReadOnlyList<StockAlert>> GetTriggeredAlertsAsync();
    Task<IReadOnlyList<StockAlert>> GetAlertsBySKUAsync(string sku);
    Task<StockAlert?> GetActiveAlertForItemAsync(int inventoryItemId);
    Task<bool> ResolveAlertAsync(int alertId, string resolvedBy);
}
