using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Infrastructure.Scraping.Interfaces;

namespace HotPepperSearch.Infrastructure.Scraping.Services;

public class AngleSharpHtmlParserService : IHtmlParserService
{
    public Task<IEnumerable<Salon>> ParseSalonListAsync(string html, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
