# TICKET-002: Inventory Management System Implementation

## Overview
Created new Inventory Service microservice for comprehensive stock tracking, stock movements, and low-stock alerts with SKU-level granularity.

## Service Architecture

### Technology Stack
- **Service Type**: Microservice (Clean Architecture)
- **Database**: SQL Server (Entity Framework Core)
- **Communication**: REST APIs + RabbitMQ Events
- **Patterns**: CQRS, Repository, Domain-Driven Design

### Project Structure
```
Services/Inventory/
├── Inventory.Core/
│   ├── Entities/
│   │   ├── EntityBase.cs
│   │   ├── InventoryItem.cs
│   │   ├── StockMovement.cs
│   │   └── StockAlert.cs
│   ├── Enums/
│   │   ├── StockMovementType.cs
│   │   └── AlertStatus.cs
│   └── Repositories/
│       ├── IAsyncRepository.cs
│       ├── IInventoryRepository.cs
│       ├── IStockMovementRepository.cs
│       └── IStockAlertRepository.cs
├── Inventory.Application/
│   ├── Commands/
│   │   ├── AddInventoryItemCommand.cs
│   │   ├── UpdateStockCommand.cs
│   │   ├── AdjustStockCommand.cs
│   │   ├── ReserveStockCommand.cs
│   │   └── ReleaseStockCommand.cs
│   ├── Queries/
│   │   ├── GetInventoryItemQuery.cs
│   │   ├── GetInventoryBySKUQuery.cs
│   │   ├── GetLowStockItemsQuery.cs
│   │   ├── GetStockMovementHistoryQuery.cs
│   │   └── GetActiveAlertsQuery.cs
│   └── Responses/
│       ├── InventoryItemResponse.cs
│       ├── StockMovementResponse.cs
│       └── StockAlertResponse.cs
├── Inventory.Infrastructure/
│   ├── Data/
│   ├── Repositories/
│   └── (EF Core implementations)
└── Inventory.API/
    └── Controllers/
        └── (REST endpoints)
```

## Core Entities

### 1. InventoryItem
Tracks stock for each product/variation SKU.

**Key Fields:**
- `ProductId`, `VariationId`, `SKU` - Product identification
- `QuantityOnHand` - Physical stock count
- `QuantityReserved` - Stock reserved for pending orders
- `QuantityAvailable` - Calculated (OnHand - Reserved)
- `ReorderPoint` - Threshold to trigger reorder
- `ReorderQuantity` - Suggested reorder amount
- `LowStockThreshold` - Alert trigger level
- `WarehouseId`, `Location` - Physical location
- `IsActive` - Enable/disable tracking

### 2. StockMovement
Audit trail of all stock changes.

**Key Fields:**
- `InventoryItemId`, `SKU` - Item reference
- `MovementType` - Enum (Addition, Removal, Sale, Return, Transfer, Adjustment, Reserved, Released)
- `Quantity` - Amount changed
- `PreviousQuantity`, `NewQuantity` - Before/after state
- `ReferenceType`, `ReferenceId` - Order/PO/Transfer reference
- `FromWarehouseId`, `ToWarehouseId` - Transfer tracking
- `Reason`, `Notes` - Audit information
- `MovementDate`, `CreatedBy` - Timestamp and actor

### 3. StockAlert
Low stock notifications and tracking.

**Key Fields:**
- `InventoryItemId`, `SKU` - Item reference
- `Status` - Enum (Active, Triggered, Resolved, Disabled)
- `CurrentQuantity`, `ThresholdQuantity` - Alert condition
- `AlertType` - LowStock, OutOfStock, ReorderPoint
- `NotificationSentTo`, `NotificationSentDate` - Notification tracking
- `ResolvedDate`, `ResolvedBy` - Resolution tracking

## Key Features

### ✅ Stock Tracking
- **Per-SKU Granularity**: Track inventory for each product variation
- **Real-Time Updates**: Immediate stock level updates
- **Reserved Stock**: Separate tracking for pending order reservations
- **Multi-Warehouse Support**: Track stock across locations

### ✅ Stock Movements
- **Complete Audit Trail**: Every stock change logged
- **Movement Types**: Addition, Removal, Sale, Return, Transfer, Adjustment, Reserved, Released
- **Reference Tracking**: Link movements to orders, POs, transfers
- **History Queries**: View movement history by SKU, date range, type

### ✅ Alert System
- **Configurable Thresholds**: Set reorder points and low stock levels per item
- **Alert Types**: LowStock, OutOfStock, ReorderPoint
- **Notification Tracking**: Track alert notifications sent
- **Alert Resolution**: Mark alerts as resolved when restocked

### ✅ Stock Operations
- **Add/Remove Stock**: Adjust quantities with reason tracking
- **Reserve/Release Stock**: Order fulfillment workflow
- **Stock Adjustments**: Manual corrections with audit trail
- **Bulk Operations**: Support for batch updates

## API Endpoints

### Inventory Management
```
POST   /Inventory/items                     - Create inventory item
GET    /Inventory/items/{id}                - Get item by ID
GET    /Inventory/items/sku/{sku}           - Get item by SKU
GET    /Inventory/items/product/{productId} - Get items for product
PUT    /Inventory/items/{id}                - Update inventory item
DELETE /Inventory/items/{id}                - Delete inventory item
```

### Stock Operations
```
POST   /Inventory/stock/update              - Add/remove stock
POST   /Inventory/stock/adjust              - Adjust stock to specific quantity
POST   /Inventory/stock/reserve             - Reserve stock for order
POST   /Inventory/stock/release             - Release reserved stock
GET    /Inventory/stock/low                 - Get low stock items
GET    /Inventory/stock/outofstock          - Get out-of-stock items
GET    /Inventory/stock/reorder             - Get items below reorder point
```

### Stock Movement History
```
GET    /Inventory/movements/sku/{sku}       - Get movements for SKU
GET    /Inventory/movements/product/{id}    - Get movements for product
GET    /Inventory/movements/type/{type}     - Get movements by type
GET    /Inventory/movements/date            - Get movements by date range
GET    /Inventory/movements/reference       - Get movements by reference
```

### Alerts
```
GET    /Inventory/alerts/active             - Get active alerts
GET    /Inventory/alerts/triggered          - Get triggered alerts
GET    /Inventory/alerts/sku/{sku}          - Get alerts for SKU
POST   /Inventory/alerts/{id}/resolve       - Resolve alert
```

## Integration Points

### 1. Catalog Service Integration
**Event: ProductVariationCreated**
- Auto-create inventory item when product variation added
- Sync SKU, product details, initial stock

**Event: ProductVariationUpdated**
- Update inventory item details when variation changes

**Event: ProductVariationDeleted**
- Mark inventory item as inactive

### 2. Ordering Service Integration
**Event: OrderCreated**
- Reserve stock for order items
- Create StockMovement records (Type: Reserved)
- Check stock availability, reject if insufficient

**Event: OrderCancelled**
- Release reserved stock
- Create StockMovement records (Type: Released)

**Event: OrderCompleted**
- Convert reserved to sold
- Create StockMovement records (Type: Sale)

### 3. Supplier Service Integration (Future)
**Event: PurchaseOrderReceived**
- Add stock when PO received
- Create StockMovement records (Type: Addition)
- Auto-resolve low stock alerts

### 4. Alert Notifications
**Email/SMS Integration**
- Send notifications when alerts triggered
- Daily/weekly low stock reports
- Reorder suggestions

## Database Schema (SQL Server)

### InventoryItems Table
```sql
CREATE TABLE InventoryItems (
    Id INT PRIMARY KEY IDENTITY,
    ProductId NVARCHAR(50) NOT NULL,
    ProductName NVARCHAR(255) NOT NULL,
    VariationId NVARCHAR(50) NULL,
    SKU NVARCHAR(100) UNIQUE NOT NULL,
    QuantityOnHand INT NOT NULL DEFAULT 0,
    QuantityReserved INT NOT NULL DEFAULT 0,
    ReorderPoint INT NOT NULL DEFAULT 10,
    ReorderQuantity INT NOT NULL DEFAULT 50,
    LowStockThreshold INT NOT NULL DEFAULT 5,
    WarehouseId NVARCHAR(50) NULL,
    WarehouseName NVARCHAR(255) NULL,
    Location NVARCHAR(255) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    LastStockUpdateDate DATETIME2 NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(100) NULL,
    LastModifiedDate DATETIME2 NULL,
    LastModifiedBy NVARCHAR(100) NULL,
    INDEX IX_ProductId (ProductId),
    INDEX IX_SKU (SKU),
    INDEX IX_VariationId (VariationId),
    INDEX IX_WarehouseId (WarehouseId)
);
```

### StockMovements Table
```sql
CREATE TABLE StockMovements (
    Id INT PRIMARY KEY IDENTITY,
    InventoryItemId INT NOT NULL,
    SKU NVARCHAR(100) NOT NULL,
    ProductId NVARCHAR(50) NOT NULL,
    VariationId NVARCHAR(50) NULL,
    MovementType INT NOT NULL,
    Quantity INT NOT NULL,
    PreviousQuantity INT NOT NULL,
    NewQuantity INT NOT NULL,
    ReferenceType NVARCHAR(50) NULL,
    ReferenceId NVARCHAR(100) NULL,
    FromWarehouseId NVARCHAR(50) NULL,
    ToWarehouseId NVARCHAR(50) NULL,
    Location NVARCHAR(255) NULL,
    Reason NVARCHAR(500) NULL,
    Notes NVARCHAR(MAX) NULL,
    MovementDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(100) NULL,
    FOREIGN KEY (InventoryItemId) REFERENCES InventoryItems(Id),
    INDEX IX_InventoryItemId (InventoryItemId),
    INDEX IX_SKU (SKU),
    INDEX IX_MovementType (MovementType),
    INDEX IX_MovementDate (MovementDate),
    INDEX IX_Reference (ReferenceType, ReferenceId)
);
```

### StockAlerts Table
```sql
CREATE TABLE StockAlerts (
    Id INT PRIMARY KEY IDENTITY,
    InventoryItemId INT NOT NULL,
    SKU NVARCHAR(100) NOT NULL,
    ProductName NVARCHAR(255) NOT NULL,
    Status INT NOT NULL,
    CurrentQuantity INT NOT NULL,
    ThresholdQuantity INT NOT NULL,
    AlertType NVARCHAR(50) NOT NULL,
    NotificationSentTo NVARCHAR(500) NULL,
    NotificationSentDate DATETIME2 NULL,
    ResolvedDate DATETIME2 NULL,
    ResolvedBy NVARCHAR(100) NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(100) NULL,
    FOREIGN KEY (InventoryItemId) REFERENCES InventoryItems(Id),
    INDEX IX_Status (Status),
    INDEX IX_AlertType (AlertType),
    INDEX IX_InventoryItemId (InventoryItemId)
);
```

## Workflow Examples

### Example 1: Product Variation Created
```
1. Admin creates product variation in Catalog Service
2. Catalog publishes ProductVariationCreated event
3. Inventory Service consumes event
4. Auto-creates InventoryItem with SKU from variation
5. Sets initial stock to 0, default thresholds
```

### Example 2: Customer Places Order
```
1. Customer adds items to cart with specific SKUs
2. Checkout process calls Reserve Stock API
3. Inventory Service validates availability
4. If available:
   - Increments QuantityReserved
   - Creates StockMovement (Type: Reserved)
   - Returns success
5. If insufficient:
   - Returns error with available quantity
   - Order creation fails
```

### Example 3: Low Stock Alert
```
1. Stock update reduces quantity below LowStockThreshold
2. Alert service checks thresholds
3. Creates StockAlert (Status: Triggered, Type: LowStock)
4. Sends notification email to inventory manager
5. Sets NotificationSentDate
6. Manager receives email with reorder suggestion
7. Manager restocks and resolves alert
```

### Example 4: Stock Adjustment
```
1. Admin performs physical inventory count
2. Finds discrepancy (System: 100, Physical: 95)
3. Calls Adjust Stock API
4. Creates StockMovement:
   - Type: Adjustment
   - PreviousQuantity: 100
   - NewQuantity: 95
   - Quantity: -5
   - Reason: "Physical inventory count"
5. Updates InventoryItem.QuantityOnHand to 95
```

## Implementation Status

### ✅ Completed
- Core entities and enums
- Repository interfaces
- CQRS command/query definitions
- Response DTOs
- Service architecture design

### ⏳ Pending (Handler implementations follow Catalog Service pattern)
- Command/Query handlers
- Repository EF Core implementations
- InventoryController REST APIs
- RabbitMQ event consumers
- Alert notification service
- EF Core migrations
- Docker configuration
- Integration tests

## Next Steps

1. **Implement Handlers**: Create MediatR handlers for all commands/queries
2. **EF Core Setup**: Configure DbContext, migrations, repositories
3. **API Layer**: Build InventoryController with all endpoints
4. **Event Integration**: Consume Catalog/Ordering events via RabbitMQ
5. **Alert Service**: Background service for alert monitoring/notifications
6. **Docker Config**: Add to docker-compose.yml with SQL Server
7. **API Gateway**: Register routes in Ocelot configuration
8. **Testing**: Unit tests for handlers, integration tests for workflows

## Technical Notes

### Performance Considerations
- Index on SKU for fast lookups
- Index on ProductId/VariationId for product queries
- Index on MovementDate for historical queries
- Pagination for large result sets
- Caching for frequently accessed items

### Data Integrity
- Foreign key constraints between tables
- Transaction wrapping for multi-step operations
- Optimistic concurrency for stock updates
- Audit fields on all entities

### Scalability
- Separate read/write models (CQRS)
- Async operations throughout
- Event-driven integration (loose coupling)
- Horizontal scaling via Docker/Kubernetes
- Database sharding by warehouse (future)

---

**Implemented By**: Claude Code
**Branch**: `feature/TICKET-002-inventory-management`
**Status**: 🏗️ Foundation Complete | ⏳ Handlers/APIs Pending
**Estimated Completion**: 40% (Core structure and contracts ready)
