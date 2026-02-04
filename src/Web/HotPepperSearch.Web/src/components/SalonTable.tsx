import type { SalonResponse, SortField, SortOrder } from '../types';

interface SalonTableProps {
  salons: SalonResponse[];
  onSort: (field: SortField, order: SortOrder) => void;
  currentSortField?: SortField;
  currentSortOrder?: SortOrder;
  isLoading?: boolean;
}

export function SalonTable({
  salons,
  onSort,
  currentSortField,
  currentSortOrder,
  isLoading = false
}: SalonTableProps) {
  const handleSort = (field: SortField) => {
    const newOrder: SortOrder =
      currentSortField === field && currentSortOrder === 'Descending'
        ? 'Ascending'
        : 'Descending';
    onSort(field, newOrder);
  };

  const getSortIcon = (field: SortField) => {
    if (currentSortField !== field) return '↕';
    return currentSortOrder === 'Ascending' ? '↑' : '↓';
  };

  if (salons.length === 0) {
    return <p className="no-results">検索結果がありません</p>;
  }

  return (
    <div className="salon-table-container">
      <div className="sort-buttons">
        <span>並び替え:</span>
        <button
          onClick={() => handleSort('Rating')}
          className={currentSortField === 'Rating' ? 'active' : ''}
          disabled={isLoading}
        >
          評価 {getSortIcon('Rating')}
        </button>
        <button
          onClick={() => handleSort('ReviewCount')}
          className={currentSortField === 'ReviewCount' ? 'active' : ''}
          disabled={isLoading}
        >
          口コミ数 {getSortIcon('ReviewCount')}
        </button>
        <button
          onClick={() => handleSort('Price')}
          className={currentSortField === 'Price' ? 'active' : ''}
          disabled={isLoading}
        >
          価格 {getSortIcon('Price')}
        </button>
      </div>

      <table className="salon-table">
        <thead>
          <tr>
            <th>サロン名</th>
            <th>ジャンル</th>
            <th>エリア</th>
            <th>最寄り駅</th>
            <th>評価</th>
            <th>口コミ</th>
            <th>価格帯</th>
          </tr>
        </thead>
        <tbody>
          {salons.map((salon) => (
            <tr key={salon.id}>
              <td>
                {salon.url ? (
                  <a href={salon.url} target="_blank" rel="noopener noreferrer">
                    {salon.name}
                  </a>
                ) : (
                  salon.name
                )}
              </td>
              <td>{salon.genre}</td>
              <td>{salon.area || '-'}</td>
              <td>{salon.nearestStation || '-'}</td>
              <td>{salon.rating?.toFixed(1) || '-'}</td>
              <td>{salon.reviewCount}</td>
              <td>{salon.priceRange || '-'}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <p className="results-count">{salons.length}件のサロン</p>
    </div>
  );
}
