namespace Inventory.Core.Enums;

public enum StockMovementType
{
    Addition = 1,           // Stock added (purchase, restock)
    Removal = 2,            // Stock removed (damage, loss)
    Sale = 3,               // Stock sold to customer
    Return = 4,             // Customer return
    Transfer = 5,           // Transfer between warehouses
    Adjustment = 6,         // Manual adjustment/correction
    Reserved = 7,           // Reserved for pending order
    Released = 8            // Released from reservation
}
