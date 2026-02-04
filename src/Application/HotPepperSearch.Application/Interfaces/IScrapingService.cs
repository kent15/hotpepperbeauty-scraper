using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Interfaces;

public interface IScrapingService
{
    Task<IEnumerable<Salon>> ScrapeAsync(SearchCondition condition, CancellationToken cancellationToken = default);
}
