using HotPepperSearch.Application.Common;
using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace HotPepperSearch.Application.Services;

public class SalonSearchService : ISalonSearchService
{
    private readonly IScrapingService _scrapingService;
    private readonly ILogger<SalonSearchService> _logger;

    public SalonSearchService(
        IScrapingService scrapingService,
        ILogger<SalonSearchService> logger)
    {
        _scrapingService = scrapingService;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Salon>>> SearchSalonsByConditionAsync(SearchCondition condition, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting salon search with condition: {@Condition}", condition);

        try
        {
            var salons = await _scrapingService.ScrapeAsync(condition, cancellationToken);
            _logger.LogInformation("Search completed. Found {Count} salons", salons.Count());
            return Result<IEnumerable<Salon>>.Success(salons);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Network error during salon search");
            return Result<IEnumerable<Salon>>.Failure("ネットワークエラーが発生しました");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during salon search");
            return Result<IEnumerable<Salon>>.Failure("検索中にエラーが発生しました");
        }
    }
}
