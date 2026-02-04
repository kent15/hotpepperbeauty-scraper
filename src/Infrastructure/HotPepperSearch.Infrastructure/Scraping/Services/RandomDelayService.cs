using HotPepperSearch.Infrastructure.Scraping.Configuration;
using HotPepperSearch.Infrastructure.Scraping.Interfaces;
using Microsoft.Extensions.Options;

namespace HotPepperSearch.Infrastructure.Scraping.Services;

public class RandomDelayService : IDelayService
{
    private readonly ScrapingSettings _settings;

    public RandomDelayService(IOptions<ScrapingSettings> settings)
    {
        _settings = settings.Value;
    }

    public Task DelayAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
