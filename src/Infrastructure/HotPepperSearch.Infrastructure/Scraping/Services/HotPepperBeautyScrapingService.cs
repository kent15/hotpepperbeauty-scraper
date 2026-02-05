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
        var serviceCode = GetServiceCode(condition.Genre);
        var (macroArea, prefCode) = GetAreaCodes(condition.Prefecture);

        // URL format: https://beauty.hotpepper.jp/svcSA/macAB/pre13/
        var url = $"{_settings.BaseUrl}{serviceCode}/{macroArea}/{prefCode}/";

        if (page > 1)
        {
            url += $"PN{page}.htm";
        }

        return url;
    }

    private static string GetServiceCode(Genre? genre)
    {
        // HotPepper Beauty service codes
        return genre switch
        {
            Genre.HairSalon => "svcSA",
            Genre.Nail => "svcSB",
            Genre.Esthe => "svcSC",
            Genre.Relaxation => "svcSD",
            Genre.EyeBeauty => "svcSE",
            _ => "svcSA"
        };
    }

    private static (string MacroArea, string PrefCode) GetAreaCodes(string? prefecture)
    {
        // Default to Tokyo if no prefecture specified
        if (string.IsNullOrEmpty(prefecture))
        {
            return ("macAB", "pre13");
        }

        // HotPepper Beauty area codes
        // macXX = macro area (region), preXX = prefecture code
        return prefecture switch
        {
            "東京都" => ("macAB", "pre13"),
            "神奈川県" => ("macAB", "pre14"),
            "千葉県" => ("macAB", "pre12"),
            "埼玉県" => ("macAB", "pre11"),
            "大阪府" => ("macAE", "pre27"),
            "京都府" => ("macAE", "pre26"),
            "兵庫県" => ("macAE", "pre28"),
            "愛知県" => ("macAD", "pre23"),
            "福岡県" => ("macAG", "pre40"),
            "北海道" => ("macAA", "pre01"),
            "宮城県" => ("macAA", "pre04"),
            _ => ("macAB", "pre13") // Default to Tokyo
        };
    }
}
