// Review Service Models

export interface IProductReview {
  id: string;
  productId: string;
  productName: string;
  variationId?: string;
  sku?: string;
  userId: string;
  userName: string;
  rating: number;
  title: string;
  reviewText: string;
  pros?: string;
  cons?: string;
  isVerifiedPurchase: boolean;
  purchaseDate?: Date;
  imageUrls: string[];
  status: string;
  isVisible: boolean;
  helpfulCount: number;
  notHelpfulCount: number;
  reviewDate: Date;
}

export interface ICreateReviewRequest {
  productId: string;
  variationId?: string;
  sku?: string;
  userId: string;
  userName: string;
  userEmail: string;
  rating: number;
  title: string;
  reviewText: string;
  pros?: string;
  cons?: string;
  isVerifiedPurchase: boolean;
  orderId?: number;
  imageUrls?: string[];
}

export interface IReviewStats {
  productId: string;
  averageRating: number;
  totalReviews: number;
  ratingDistribution: {
    fiveStar: number;
    fourStar: number;
    threeStar: number;
    twoStar: number;
    oneStar: number;
  };
  verifiedPurchasePercentage: number;
}
