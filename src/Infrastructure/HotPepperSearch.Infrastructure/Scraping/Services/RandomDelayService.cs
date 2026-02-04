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

    public async Task DelayAsync(CancellationToken cancellationToken = default)
    {
        var delaySeconds = Random.Shared.Next(_settings.MinDelay, _settings.MaxDelay + 1);
        await Task.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken);
    }
}
