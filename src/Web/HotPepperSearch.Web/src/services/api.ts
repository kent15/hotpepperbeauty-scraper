import type { SearchRequest, SalonResponse } from '../types';

const API_BASE_URL = '/api';

export async function searchSalons(
  request: SearchRequest
): Promise<SalonResponse[]> {
  const response = await fetch(`${API_BASE_URL}/salons/search`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(request)
  });

  if (!response.ok) {
    const error = await response.json();
    throw new Error(error.error || '検索に失敗しました');
  }

  return response.json();
}
