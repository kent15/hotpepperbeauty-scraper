import { useState } from 'react';
import type { SearchRequest, SalonResponse, SortField, SortOrder } from '../types';
import { searchSalons, sortSalons } from '../services/api';

export function useSearch() {
  const [isLoading, setIsLoading] = useState(false);
  const [isSorting, setIsSorting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [results, setResults] = useState<SalonResponse[]>([]);
  const [sortField, setSortField] = useState<SortField | undefined>();
  const [sortOrder, setSortOrder] = useState<SortOrder | undefined>();

  const search = async (request: SearchRequest) => {
    setIsLoading(true);
    setError(null);
    setSortField(undefined);
    setSortOrder(undefined);

    try {
      const data = await searchSalons(request);
      setResults(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : '検索に失敗しました');
      setResults([]);
    } finally {
      setIsLoading(false);
    }
  };

  const sort = async (field: SortField, order: SortOrder) => {
    if (results.length === 0) return;

    setIsSorting(true);
    setError(null);

    try {
      const data = await sortSalons({
        field,
        order,
        salons: results
      });
      setResults(data);
      setSortField(field);
      setSortOrder(order);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'ソートに失敗しました');
    } finally {
      setIsSorting(false);
    }
  };

  return {
    isLoading,
    isSorting,
    error,
    results,
    sortField,
    sortOrder,
    search,
    sort
  };
}
