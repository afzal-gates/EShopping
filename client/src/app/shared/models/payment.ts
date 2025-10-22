// Payment Service Models

export interface ICreatePaymentIntent {
  orderId: number;
  orderNumber: string;
  amount: number;
  currency: string;
  paymentGateway: string;
  userId: string;
  userEmail: string;
  description?: string;
  customerIP?: string;
  userAgent?: string;
}

export interface IPaymentIntentResponse {
  paymentIntentId: string;
  clientSecret: string;
  amount: number;
  currency: string;
  status: string;
  paymentGateway: string;
  is3DSecureRequired: boolean;
}

export interface IPaymentTransaction {
  id: number;
  orderId: number;
  orderNumber: string;
  paymentGateway: string;
  paymentMethod: string;
  transactionId: string;
  paymentIntentId: string;
  amount: number;
  currency: string;
  status: string;
  transactionDate: Date;
  cardBrand?: string;
  cardLast4?: string;
  failureReason?: string;
}

export interface IRefundRequest {
  paymentTransactionId: number;
  refundAmount: number;
  reason: string;
  requestedBy: string;
  notes?: string;
}

export interface IRefund {
  id: number;
  paymentTransactionId: number;
  refundId: string;
  refundAmount: number;
  currency: string;
  reason: string;
  status: string;
  refundRequestDate: Date;
  estimatedDaysToRefund: number;
}
