using Shipping.Core.Entities;
using Shipping.Core.Enums;

namespace Shipping.Core.Repositories;

public interface IShipmentRepository : IAsyncRepository<Shipment>
{
    Task<Shipment?> GetByTrackingNumberAsync(string trackingNumber);
    Task<Shipment?> GetByOrderIdAsync(int orderId);
    Task<IReadOnlyList<Shipment>> GetByStatusAsync(ShipmentStatus status);
    Task<IReadOnlyList<Shipment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
}
