using HotPepperSearch.Domain.Entities;

namespace HotPepperSearch.Infrastructure.Scraping.Interfaces;

public interface IHtmlParserService
{
    Task<IEnumerable<Salon>> ParseSalonListAsync(string html, CancellationToken cancellationToken = default);
}
