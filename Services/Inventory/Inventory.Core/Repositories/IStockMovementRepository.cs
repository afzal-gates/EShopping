using Inventory.Core.Entities;
using Inventory.Core.Enums;

namespace Inventory.Core.Repositories;

public interface IStockMovementRepository : IAsyncRepository<StockMovement>
{
    Task<IReadOnlyList<StockMovement>> GetBySKUAsync(string sku);
    Task<IReadOnlyList<StockMovement>> GetByProductIdAsync(string productId);
    Task<IReadOnlyList<StockMovement>> GetByMovementTypeAsync(StockMovementType movementType);
    Task<IReadOnlyList<StockMovement>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IReadOnlyList<StockMovement>> GetByReferenceAsync(string referenceType, string referenceId);
}
