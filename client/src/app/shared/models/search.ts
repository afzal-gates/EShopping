// Search Service Models

export interface ISearchRequest {
  query?: string;
  category?: string;
  brand?: string;
  minPrice?: number;
  maxPrice?: number;
  minRating?: number;
  sizes?: string[];
  colors?: string[];
  materials?: string[];
  isAvailable?: boolean;
  isFeatured?: boolean;
  sortBy?: string;
  pageNumber?: number;
  pageSize?: number;
}

export interface ISearchResult {
  query: string;
  totalResults: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  results: IProductSearchResult[];
  facets: ISearchFacets;
  suggestions: string[];
  searchTime: number;
}

export interface IProductSearchResult {
  id: string;
  name: string;
  description: string;
  category: string;
  brand: string;
  price: number;
  imageFile: string;
  averageRating: number;
  reviewCount: number;
  isAvailable: boolean;
  isFeatured: boolean;
  searchScore: number;
}

export interface ISearchFacets {
  categories: IFacetValue[];
  brands: IFacetValue[];
  sizes: IFacetValue[];
  colors: IFacetValue[];
  materials: IFacetValue[];
  priceRange: IPriceRange;
  ratingDistribution: IRatingDistribution;
}

export interface IFacetValue {
  value: string;
  count: number;
}

export interface IPriceRange {
  min: number;
  max: number;
}

export interface IRatingDistribution {
  fiveStar: number;
  fourStar: number;
  threeStar: number;
  twoStar: number;
  oneStar: number;
}
