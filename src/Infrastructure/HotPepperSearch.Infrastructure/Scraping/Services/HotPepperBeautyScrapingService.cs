using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.Enums;
using HotPepperSearch.Domain.ValueObjects;
using HotPepperSearch.Infrastructure.Scraping.Configuration;
using HotPepperSearch.Infrastructure.Scraping.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HotPepperSearch.Infrastructure.Scraping.Services;

public class HotPepperBeautyScrapingService : Application.Interfaces.IScrapingService
{
    private readonly HttpClient _httpClient;
    private readonly IHtmlParserService _htmlParser;
    private readonly IDelayService _delayService;
    private readonly ScrapingSettings _settings;
    private readonly ILogger<HotPepperBeautyScrapingService> _logger;

    public HotPepperBeautyScrapingService(
        HttpClient httpClient,
        IHtmlParserService htmlParser,
        IDelayService delayService,
        IOptions<ScrapingSettings> settings,
        ILogger<HotPepperBeautyScrapingService> logger)
    {
        _httpClient = httpClient;
        _htmlParser = htmlParser;
        _delayService = delayService;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task<IEnumerable<Salon>> ScrapeAsync(SearchCondition condition, CancellationToken cancellationToken = default)
    {
        var allSalons = new List<Salon>();
        var currentPage = 1;

        while (currentPage <= _settings.MaxPagesPerSearch)
        {
            var url = BuildSearchUrl(condition, currentPage);
            _logger.LogInformation("Scraping page {Page}: {Url}", currentPage, url);

            var html = await FetchHtmlAsync(url, cancellationToken);
            var salons = await _htmlParser.ParseSalonListAsync(html, cancellationToken);
            var salonList = salons.ToList();

            if (salonList.Count == 0)
            {
                _logger.LogInformation("No more salons found on page {Page}", currentPage);
                break;
            }

            foreach (var salon in salonList)
            {
                salon.Genre = condition.Genre ?? Genre.HairSalon;
            }

            allSalons.AddRange(salonList);
            _logger.LogInformation("Found {Count} salons on page {Page}", salonList.Count, currentPage);

            currentPage++;

            if (currentPage <= _settings.MaxPagesPerSearch)
            {
                await _delayService.DelayAsync(cancellationToken);
            }
        }

        _logger.LogInformation("Total salons scraped: {Count}", allSalons.Count);
        return allSalons;
    }

    private async Task<string> FetchHtmlAsync(string url, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private string BuildSearchUrl(SearchCondition condition, int page)
    {
        var genrePath = GetGenrePath(condition.Genre);
        var areaPath = BuildAreaPath(condition.Prefecture, condition.City);

        var url = $"{_settings.BaseUrl}{genrePath}/{areaPath}";

        if (page > 1)
        {
            url += $"PN{page}.htm";
        }

        return url;
    }

    private static string GetGenrePath(Genre? genre)
    {
        return genre switch
        {
            Genre.HairSalon => "hair",
            Genre.Nail => "nail",
            Genre.Esthe => "esthe",
            Genre.Relaxation => "relaxation",
            Genre.EyeBeauty => "eye",
            _ => "hair"
        };
    }

    private static string BuildAreaPath(string? prefecture, string? city)
    {
        if (string.IsNullOrEmpty(prefecture))
        {
            return "SA23";
        }

        var prefCode = GetPrefectureCode(prefecture);
        if (string.IsNullOrEmpty(city))
        {
            return prefCode;
        }

        return $"{prefCode}/{city}";
    }

    private static string GetPrefectureCode(string prefecture)
    {
        return prefecture switch
        {
            "東京都" => "SA23",
            "神奈川県" => "SA24",
            "大阪府" => "SA47",
            "愛知県" => "SA35",
            "福岡県" => "SA56",
            _ => "SA23"
        };
    }
}
