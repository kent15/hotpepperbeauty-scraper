export type Genre =
  | 'HairSalon'
  | 'Nail'
  | 'Esthe'
  | 'Relaxation'
  | 'EyeBeauty';

export interface SearchRequest {
  genre?: Genre;
  prefecture?: string;
  city?: string;
  minPrice?: number;
  maxPrice?: number;
  minRating?: number;
}

export interface SearchFormErrors {
  genre?: string;
  prefecture?: string;
  city?: string;
  minPrice?: string;
  maxPrice?: string;
  minRating?: string;
}

export interface SalonResponse {
  id: string;
  name: string;
  genre: string;
  area: string | null;
  nearestStation: string | null;
  rating: number | null;
  reviewCount: number;
  priceRange: string | null;
  url: string | null;
}

export const GENRES: { value: Genre; label: string }[] = [
  { value: 'HairSalon', label: 'ヘアサロン' },
  { value: 'Nail', label: 'ネイル' },
  { value: 'Esthe', label: 'エステ' },
  { value: 'Relaxation', label: 'リラク' },
  { value: 'EyeBeauty', label: 'アイビューティー' }
];

export const PREFECTURES = [
  '東京都',
  '神奈川県',
  '大阪府',
  '愛知県',
  '福岡県'
];
