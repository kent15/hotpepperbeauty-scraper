import { useState } from 'react';
import type { SearchRequest, SearchFormErrors, Genre } from '../types';
import { GENRES, PREFECTURES } from '../types';

interface SearchFormProps {
  onSubmit: (request: SearchRequest) => void;
  isLoading?: boolean;
}

export function SearchForm({ onSubmit, isLoading = false }: SearchFormProps) {
  const [formData, setFormData] = useState<SearchRequest>({});
  const [errors, setErrors] = useState<SearchFormErrors>({});

  const validate = (): boolean => {
    const newErrors: SearchFormErrors = {};

    if (formData.minPrice !== undefined && formData.minPrice < 0) {
      newErrors.minPrice = '最低価格は0以上で入力してください';
    }

    if (formData.maxPrice !== undefined && formData.maxPrice < 0) {
      newErrors.maxPrice = '最高価格は0以上で入力してください';
    }

    if (
      formData.minPrice !== undefined &&
      formData.maxPrice !== undefined &&
      formData.minPrice > formData.maxPrice
    ) {
      newErrors.minPrice = '最低価格は最高価格以下にしてください';
    }

    if (formData.minRating !== undefined) {
      if (formData.minRating < 0 || formData.minRating > 5) {
        newErrors.minRating = '評価は0〜5の範囲で入力してください';
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (validate()) {
      onSubmit(formData);
    }
  };

  const handleGenreChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value as Genre | '';
    setFormData((prev) => ({
      ...prev,
      genre: value || undefined
    }));
  };

  const handlePrefectureChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setFormData((prev) => ({
      ...prev,
      prefecture: e.target.value || undefined,
      city: undefined
    }));
  };

  const handleCityChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({
      ...prev,
      city: e.target.value || undefined
    }));
  };

  const handleNumberChange = (field: keyof SearchRequest) => (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = e.target.value;
    setFormData((prev) => ({
      ...prev,
      [field]: value ? Number(value) : undefined
    }));
  };

  return (
    <form onSubmit={handleSubmit} className="search-form">
      <h2>サロン検索</h2>

      <div className="form-group">
        <label htmlFor="genre">ジャンル</label>
        <select
          id="genre"
          value={formData.genre || ''}
          onChange={handleGenreChange}
        >
          <option value="">選択してください</option>
          {GENRES.map((g) => (
            <option key={g.value} value={g.value}>
              {g.label}
            </option>
          ))}
        </select>
        {errors.genre && <span className="error">{errors.genre}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="prefecture">都道府県</label>
        <select
          id="prefecture"
          value={formData.prefecture || ''}
          onChange={handlePrefectureChange}
        >
          <option value="">選択してください</option>
          {PREFECTURES.map((p) => (
            <option key={p} value={p}>
              {p}
            </option>
          ))}
        </select>
        {errors.prefecture && <span className="error">{errors.prefecture}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="city">市区町村</label>
        <input
          type="text"
          id="city"
          value={formData.city || ''}
          onChange={handleCityChange}
          placeholder="例: 渋谷区"
          maxLength={50}
        />
        {errors.city && <span className="error">{errors.city}</span>}
      </div>

      <div className="form-group">
        <label>価格帯</label>
        <div className="price-range">
          <input
            type="number"
            value={formData.minPrice ?? ''}
            onChange={handleNumberChange('minPrice')}
            placeholder="最低価格"
            min={0}
          />
          <span>〜</span>
          <input
            type="number"
            value={formData.maxPrice ?? ''}
            onChange={handleNumberChange('maxPrice')}
            placeholder="最高価格"
            min={0}
          />
        </div>
        {errors.minPrice && <span className="error">{errors.minPrice}</span>}
        {errors.maxPrice && <span className="error">{errors.maxPrice}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="minRating">最低評価</label>
        <input
          type="number"
          id="minRating"
          value={formData.minRating ?? ''}
          onChange={handleNumberChange('minRating')}
          placeholder="0.0 〜 5.0"
          min={0}
          max={5}
          step={0.1}
        />
        {errors.minRating && <span className="error">{errors.minRating}</span>}
      </div>

      <button type="submit" disabled={isLoading}>
        {isLoading ? '検索中...' : '検索'}
      </button>
    </form>
  );
}
