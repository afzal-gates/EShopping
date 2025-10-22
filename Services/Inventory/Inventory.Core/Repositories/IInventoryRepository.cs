using Inventory.Core.Entities;

namespace Inventory.Core.Repositories;

public interface IInventoryRepository : IAsyncRepository<InventoryItem>
{
    Task<InventoryItem?> GetBySKUAsync(string sku);
    Task<InventoryItem?> GetByProductVariationAsync(string productId, string? variationId);
    Task<IReadOnlyList<InventoryItem>> GetByProductIdAsync(string productId);
    Task<IReadOnlyList<InventoryItem>> GetLowStockItemsAsync();
    Task<IReadOnlyList<InventoryItem>> GetOutOfStockItemsAsync();
    Task<IReadOnlyList<InventoryItem>> GetItemsBelowReorderPointAsync();
    Task<bool> UpdateStockAsync(string sku, int quantity, string reason);
    Task<bool> ReserveStockAsync(string sku, int quantity);
    Task<bool> ReleaseStockAsync(string sku, int quantity);
}
