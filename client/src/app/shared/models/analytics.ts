// Analytics Service Models

export interface IProductProfitability {
  id: number;
  productId: string;
  productName: string;
  variationId?: string;
  sku?: string;
  totalUnitsSold: number;
  totalRevenue: number;
  averageSellingPrice: number;
  totalCOGS: number;
  averageCostPerUnit: number;
  grossProfit: number;
  grossProfitMargin: number;
  netProfit: number;
  netProfitMargin: number;
  periodStart: Date;
  periodEnd: Date;
  periodType: string;
  returnCount: number;
  returnRate: number;
  discountAmount: number;
}

export interface IOrderFinancials {
  id: number;
  orderId: number;
  orderNumber: string;
  userName: string;
  orderDate: Date;
  subTotal: number;
  shippingCost: number;
  taxAmount: number;
  totalRevenue: number;
  productCOGS: number;
  shippingCOGS: number;
  paymentProcessingFee: number;
  totalCOGS: number;
  discountAmount: number;
  discountCode?: string;
  grossProfit: number;
  grossProfitMargin: number;
  netProfit: number;
  netProfitMargin: number;
  orderStatus: string;
  isCancelled: boolean;
  isRefunded: boolean;
  refundAmount: number;
}

export interface IPeriodSummary {
  id: number;
  periodStart: Date;
  periodEnd: Date;
  periodType: string;
  totalOrders: number;
  completedOrders: number;
  cancelledOrders: number;
  refundedOrders: number;
  totalRevenue: number;
  averageOrderValue: number;
  totalCOGS: number;
  totalShippingCosts: number;
  totalPaymentFees: number;
  totalOperatingExpenses: number;
  grossProfit: number;
  grossProfitMargin: number;
  netProfit: number;
  netProfitMargin: number;
  totalDiscounts: number;
  totalRefunds: number;
  uniqueCustomers: number;
  newCustomers: number;
  returningCustomers: number;
  totalProductsSold: number;
  topSellingProductId?: string;
  topSellingProductName?: string;
  topSellingProductQuantity: number;
}
