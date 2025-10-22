export interface IProductVariation {
  id: string;
  sku: string;
  size?: string;
  color?: string;
  material?: string;
  stockQuantity: number;
  priceAdjustment: number;
  imageFile?: string;
  isActive: boolean;
  productId: string;
}

export interface IProduct {
  id: string;
  name: string;
  description: string;
  imageFile: string;
  price: number;
  productType: string;
  productBrand: string;
  variations?: IProductVariation[];
  hasVariations?: boolean;
}
