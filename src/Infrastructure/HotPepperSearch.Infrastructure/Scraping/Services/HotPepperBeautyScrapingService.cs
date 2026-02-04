using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;
using HotPepperSearch.Infrastructure.Scraping.Interfaces;

namespace HotPepperSearch.Infrastructure.Scraping.Services;

public class HotPepperBeautyScrapingService : IScrapingService
{
    private readonly HttpClient _httpClient;
    private readonly IHtmlParserService _htmlParser;
    private readonly IDelayService _delayService;

    public HotPepperBeautyScrapingService(
        HttpClient httpClient,
        IHtmlParserService htmlParser,
        IDelayService delayService)
    {
        _httpClient = httpClient;
        _htmlParser = htmlParser;
        _delayService = delayService;
    }

    public Task<IEnumerable<Salon>> ScrapeAsync(SearchCondition condition, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
