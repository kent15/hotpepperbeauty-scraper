using HotPepperSearch.Domain.Entities;

namespace HotPepperSearch.Domain.Repositories;

public interface ISearchHistoryRepository
{
    Task<IEnumerable<SearchHistory>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SearchHistory> AddAsync(SearchHistory history, CancellationToken cancellationToken = default);
}
