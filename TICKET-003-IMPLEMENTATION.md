# TICKET-003: Order Status Workflow Engine Implementation

## Overview
Enhanced Ordering Service with comprehensive order status workflow, state transitions, and complete order timeline tracking.

## Order Status Workflow

###  Status States
```
Pending → PaymentReceived → Processing → Packed → Shipped → Delivered
     ↓                                                        ↓
 Cancelled/Failed                                       Refunded
```

### Status Definitions

| Status | Description | Triggers | Next Valid States |
|--------|-------------|----------|-------------------|
| **Pending** | Order created, awaiting payment | Order creation | PaymentReceived, Cancelled, Failed |
| **PaymentReceived** | Payment confirmed | Payment gateway webhook | Processing, Cancelled |
| **Processing** | Order being prepared/picked | Manual or automated | Packed, Cancelled |
| **Packed** | Order packed, ready to ship | Warehouse confirmation | Shipped, Cancelled |
| **Shipped** | Order dispatched to customer | Carrier pickup | Delivered, (Cancelled rare) |
| **Delivered** | Successfully delivered | Carrier confirmation | Refunded |
| **Cancelled** | Order cancelled | Customer/admin action | (Terminal state) |
| **Refunded** | Order refunded | Refund processed | (Terminal state) |
| **Failed** | Order failed | Payment/processing failure | (Terminal state) |

## Implementation Details

### Core Entities

#### OrderStatus Enum
```csharp
public enum OrderStatus
{
    Pending = 1,
    PaymentReceived = 2,
    Processing = 3,
    Packed = 4,
    Shipped = 5,
    Delivered = 6,
    Cancelled = 7,
    Refunded = 8,
    Failed = 9
}
```

#### Order Entity (Updated)
**New Fields:**
- `OrderStatus Status` - Current order status
- `DateTime? PaymentDate` - When payment received
- `DateTime? ShippedDate` - When order shipped
- `DateTime? DeliveredDate` - When order delivered
- `DateTime? CancelledDate` - When order cancelled
- `string? CancellationReason` - Why order was cancelled
- `string? TrackingNumber` - Shipping tracking number
- `string? CarrierName` - Shipping carrier name
- `ICollection<OrderStatusHistory> StatusHistory` - Full status timeline

#### OrderStatusHistory Entity (New)
Complete audit trail of all status changes.

**Fields:**
- `int OrderId` - Order reference
- `OrderStatus FromStatus` - Previous status
- `OrderStatus ToStatus` - New status
- `DateTime StatusChangeDate` - When changed
- `string? ChangedBy` - Who made the change
- `string? Reason` - Why status changed
- `string? Notes` - Additional context

### CQRS Implementation

#### Commands
```csharp
// Update order status with validation
UpdateOrderStatusCommand
{
    OrderId, NewStatus, Reason, Notes, ChangedBy,
    TrackingNumber, CarrierName
}

// Cancel order with reason
CancelOrderCommand
{
    OrderId, Reason, CancelledBy
}
```

#### Queries
```csharp
// Get status history timeline
GetOrderStatusHistoryQuery(orderId)
→ Returns: IList<OrderStatusHistoryResponse>
```

#### Responses
```csharp
OrderStatusHistoryResponse
{
    Id, OrderId, FromStatus, ToStatus,
    StatusChangeDate, ChangedBy, Reason, Notes
}
```

## Status Transition Rules

### Validation Matrix
```
Current Status     | Allowed Next Status
-------------------|-------------------------------------------
Pending            | PaymentReceived, Cancelled, Failed
PaymentReceived    | Processing, Cancelled
Processing         | Packed, Cancelled
Packed             | Shipped, Cancelled
Shipped            | Delivered, (Cancelled - with admin override)
Delivered          | Refunded
Cancelled          | (None - terminal)
Refunded           | (None - terminal)
Failed             | (None - terminal)
```

### Transition Side Effects

**Pending → PaymentReceived:**
- Set `PaymentDate = DateTime.UtcNow`
- Trigger inventory reservation
- Send order confirmation email

**Processing → Packed:**
- Validate all items picked
- Update inventory to reserved status
- Notify shipping department

**Packed → Shipped:**
- Set `ShippedDate`, `TrackingNumber`, `CarrierName`
- Deduct inventory (convert reserved to sold)
- Send shipping notification email
- Create shipment tracking event

**Shipped → Delivered:**
- Set `DeliveredDate`
- Release any remaining holds
- Send delivery confirmation
- Trigger review request email

**Any → Cancelled:**
- Set `CancelledDate`, `CancellationReason`
- Release reserved inventory
- Initiate refund process if payment received
- Send cancellation notification

**Delivered → Refunded:**
- Process refund to original payment method
- Update inventory (return to stock)
- Send refund confirmation

## API Endpoints

### Status Management
```
PUT    /Order/{id}/status              - Update order status
POST   /Order/{id}/cancel              - Cancel order
GET    /Order/{id}/status-history      - Get status timeline
GET    /Order/status/{status}          - Get orders by status
```

### Status Queries
```
GET    /Order/pending                  - Get pending orders
GET    /Order/processing               - Get orders being processed
GET    /Order/shipped                  - Get shipped orders
GET    /Order/delivered                - Get delivered orders
```

## Event-Driven Integration

### Published Events

**OrderStatusChanged**
```json
{
  "orderId": 123,
  "previousStatus": "Processing",
  "newStatus": "Shipped",
  "changedDate": "2025-10-22T10:30:00Z",
  "trackingNumber": "1Z999AA1234567890",
  "carrierName": "UPS"
}
```

**OrderCancelled**
```json
{
  "orderId": 123,
  "cancelledDate": "2025-10-22T10:30:00Z",
  "reason": "Customer request",
  "refundRequired": true,
  "amountToRefund": 99.99
}
```

### Consumed Events

**PaymentConfirmed** (from Payment Service)
→ Trigger: Pending → PaymentReceived

**ShipmentCreated** (from Shipping Service)
→ Trigger: Packed → Shipped

**DeliveryConfirmed** (from Carrier API)
→ Trigger: Shipped → Delivered

## Integration Points

### 1. Inventory Service Integration
```
OrderStatusChanged(Pending → PaymentReceived)
→ Reserve stock for order items

OrderStatusChanged(Packed → Shipped)
→ Deduct inventory (Reserved → Sold)

OrderCancelled
→ Release reserved stock
```

### 2. Shipping Service Integration
```
OrderStatusChanged(Processing → Packed)
→ Create shipment in shipping service

OrderStatusChanged(Packed → Shipped)
→ Update shipment with tracking details
```

### 3. Notification Service Integration
```
OrderStatusChanged (any transition)
→ Send status update email/SMS to customer

OrderStatusChanged(Shipped)
→ Send tracking link and estimated delivery

OrderStatusChanged(Delivered)
→ Request product review
```

### 4. Payment Service Integration
```
OrderCancelled (PaymentReceived or later)
→ Initiate refund process

OrderStatusChanged(Delivered → Refunded)
→ Process refund to original payment method
```

## Database Schema Updates

### Orders Table (Columns Added)
```sql
ALTER TABLE Orders ADD COLUMN Status INT NOT NULL DEFAULT 1;
ALTER TABLE Orders ADD COLUMN PaymentDate DATETIME2 NULL;
ALTER TABLE Orders ADD COLUMN ShippedDate DATETIME2 NULL;
ALTER TABLE Orders ADD COLUMN DeliveredDate DATETIME2 NULL;
ALTER TABLE Orders ADD COLUMN CancelledDate DATETIME2 NULL;
ALTER TABLE Orders ADD COLUMN CancellationReason NVARCHAR(500) NULL;
ALTER TABLE Orders ADD COLUMN TrackingNumber NVARCHAR(100) NULL;
ALTER TABLE Orders ADD COLUMN CarrierName NVARCHAR(100) NULL;

CREATE INDEX IX_Orders_Status ON Orders(Status);
CREATE INDEX IX_Orders_ShippedDate ON Orders(ShippedDate);
CREATE INDEX IX_Orders_DeliveredDate ON Orders(DeliveredDate);
```

### OrderStatusHistory Table (New)
```sql
CREATE TABLE OrderStatusHistory (
    Id INT PRIMARY KEY IDENTITY,
    OrderId INT NOT NULL,
    FromStatus INT NOT NULL,
    ToStatus INT NOT NULL,
    StatusChangeDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ChangedBy NVARCHAR(100) NULL,
    Reason NVARCHAR(500) NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(100) NULL,
    LastModifiedDate DATETIME2 NULL,
    LastModifiedBy NVARCHAR(100) NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    INDEX IX_OrderStatusHistory_OrderId (OrderId),
    INDEX IX_OrderStatusHistory_ToStatus (ToStatus),
    INDEX IX_OrderStatusHistory_StatusChangeDate (StatusChangeDate)
);
```

## Workflow Examples

### Example 1: Happy Path
```
1. Customer places order
   → Status: Pending

2. Payment gateway confirms payment
   → Status: PaymentReceived
   → PaymentDate set
   → Inventory reserved

3. Warehouse picks and packs items
   → Status: Processing → Packed
   → All items confirmed

4. Carrier picks up package
   → Status: Shipped
   → ShippedDate, TrackingNumber, CarrierName set
   → Inventory deducted
   → Customer notified with tracking

5. Package delivered
   → Status: Delivered
   → DeliveredDate set
   → Review request sent
```

### Example 2: Cancellation Flow
```
1. Customer places order
   → Status: Pending

2. Payment confirmed
   → Status: PaymentReceived
   → Inventory reserved

3. Customer cancels before shipping
   → Status: Cancelled
   → CancelledDate, CancellationReason set
   → Inventory released
   → Refund initiated
   → Cancellation email sent
```

### Example 3: Refund After Delivery
```
1. Order delivered successfully
   → Status: Delivered

2. Customer requests refund (defective item)
   → Admin approves refund
   → Status: Refunded
   → Refund processed to payment method
   → Inventory returned to stock
   → Refund confirmation sent
```

## Admin Dashboard Features

### Order Status Dashboard
- **Status Overview**: Count of orders in each status
- **Status Timeline**: Average time in each status
- **Bottleneck Detection**: Identify status stuck longer than expected
- **Bulk Status Updates**: Update multiple orders at once

### Metrics Tracked
- Average time from Pending → Delivered
- Orders stuck in Processing > 24 hours
- Cancellation rate by status
- Refund rate
- On-time delivery rate

## Implementation Status

### ✅ Completed
- OrderStatus enum with 9 states
- Order entity updates with status tracking
- OrderStatusHistory entity for audit trail
- UpdateOrderStatusCommand
- CancelOrderCommand
- GetOrderStatusHistoryQuery
- OrderStatusHistoryResponse DTO

### ⏳ Pending
- Status transition validation logic
- Command/query handlers
- OrderController status endpoints
- RabbitMQ event publishers/consumers
- Email/SMS notification triggers
- EF Core migrations
- Admin dashboard queries
- Status workflow unit tests

## Next Steps

1. **Validation Service**: Implement status transition rules engine
2. **Handlers**: Create command/query handlers with validation
3. **Events**: Publish OrderStatusChanged events via RabbitMQ
4. **Notifications**: Integrate email/SMS for status updates
5. **API Endpoints**: Add status management routes to OrderController
6. **Dashboard Queries**: Create queries for admin analytics
7. **Testing**: Unit tests for workflow validations
8. **Documentation**: API documentation with workflow diagrams

## Technical Notes

### Concurrency Handling
- Use optimistic locking for order updates
- Prevent concurrent status changes via version checking
- Queue status updates if high volume

### Performance
- Index on Status for filtering
- Paginate status history for orders with many changes
- Cache status counts for dashboard

### Audit & Compliance
- Complete audit trail via StatusHistory
- Immutable history records
- Track all status changes with actor/reason

---

**Implemented By**: Claude Code
**Branch**: `feature/TICKET-003-order-status-workflow`
**Status**: 🏗️ Foundation Complete | ⏳ Handlers/Events Pending
**Estimated Completion**: 35% (Core entities and commands ready)
