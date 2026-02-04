import { SearchForm } from '../components/SearchForm';
import { SalonTable } from '../components/SalonTable';
import { useSearch } from '../hooks/useSearch';

export function SearchPage() {
  const {
    isLoading,
    isSorting,
    error,
    results,
    sortField,
    sortOrder,
    search,
    sort
  } = useSearch();

  return (
    <div className="search-page">
      <SearchForm onSubmit={search} isLoading={isLoading} />

      {error && <div className="error-message">{error}</div>}

      {results.length > 0 && (
        <SalonTable
          salons={results}
          onSort={sort}
          currentSortField={sortField}
          currentSortOrder={sortOrder}
          isLoading={isSorting}
        />
      )}
    </div>
  );
}
