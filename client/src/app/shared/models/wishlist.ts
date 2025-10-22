// WishList Service Models

export interface IWishList {
  id: string;
  userId: string;
  userName: string;
  name: string;
  isPublic: boolean;
  shareToken?: string;
  items: IWishListItem[];
  createdDate: Date;
  totalItems: number;
  totalValue: number;
}

export interface IWishListItem {
  itemId: string;
  productId: string;
  productName: string;
  variationId?: string;
  sku?: string;
  price: number;
  originalPrice: number;
  imageUrl?: string;
  quantity: number;
  priority: number;
  notes?: string;
  isAvailable: boolean;
  addedDate: Date;
  priceAlertEnabled: boolean;
  targetPrice?: number;
  alertSent: boolean;
  isPriceDropped: boolean;
}

export interface IAddToWishListRequest {
  userId: string;
  productId: string;
  variationId?: string;
  sku?: string;
  quantity?: number;
  priority?: number;
  enablePriceAlert?: boolean;
  targetPrice?: number;
  notes?: string;
}

export interface IMoveToCartRequest {
  userId: string;
  wishListItemId: string;
}
