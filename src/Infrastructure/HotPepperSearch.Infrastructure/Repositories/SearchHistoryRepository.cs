using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.Repositories;
using HotPepperSearch.Infrastructure.Data;

namespace HotPepperSearch.Infrastructure.Repositories;

public class SearchHistoryRepository : ISearchHistoryRepository
{
    private readonly ApplicationDbContext _context;

    public SearchHistoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<SearchHistory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<SearchHistory> AddAsync(SearchHistory history, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
