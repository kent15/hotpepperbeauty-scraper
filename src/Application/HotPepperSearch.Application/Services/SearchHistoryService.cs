using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Services;

public class SearchHistoryService : ISearchHistoryService
{
    public Task<IEnumerable<SearchHistory>> GetSearchHistoryAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SaveSearchHistoryAsync(SearchCondition condition, int resultCount, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
