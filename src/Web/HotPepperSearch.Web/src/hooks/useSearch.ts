import { useState } from 'react';
import type { SearchRequest, SalonResponse } from '../types';
import { searchSalons } from '../services/api';

export function useSearch() {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [results, setResults] = useState<SalonResponse[]>([]);

  const search = async (request: SearchRequest) => {
    setIsLoading(true);
    setError(null);

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

  return {
    isLoading,
    error,
    results,
    search
  };
}
