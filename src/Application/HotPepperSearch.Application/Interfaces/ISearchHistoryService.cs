using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Interfaces;

public interface ISearchHistoryService
{
    Task<IEnumerable<SearchHistory>> GetSearchHistoryAsync(CancellationToken cancellationToken = default);
    Task SaveSearchHistoryAsync(SearchCondition condition, int resultCount, CancellationToken cancellationToken = default);
}
