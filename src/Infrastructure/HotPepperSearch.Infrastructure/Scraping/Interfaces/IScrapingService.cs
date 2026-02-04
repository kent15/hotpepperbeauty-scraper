using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Infrastructure.Scraping.Interfaces;

public interface IScrapingService
{
    Task<IEnumerable<Salon>> ScrapeAsync(SearchCondition condition, CancellationToken cancellationToken = default);
}
