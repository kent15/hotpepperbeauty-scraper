import { SearchForm } from '../components/SearchForm';
import { useSearch } from '../hooks/useSearch';

export function SearchPage() {
  const { isLoading, error, results, search } = useSearch();

  return (
    <div className="search-page">
      <SearchForm onSubmit={search} isLoading={isLoading} />

      {error && <div className="error-message">{error}</div>}

      {results.length > 0 && (
        <div className="results-summary">
          <p>{results.length}件のサロンが見つかりました</p>
        </div>
      )}
    </div>
  );
}
