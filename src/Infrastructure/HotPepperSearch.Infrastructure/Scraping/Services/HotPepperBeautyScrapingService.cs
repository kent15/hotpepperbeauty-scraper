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
            _logger.LogDebug("Received HTML length: {Length} characters", html.Length);

            // Debug: Log first 500 chars of HTML to verify content
            if (html.Length > 0)
            {
                var preview = html.Length > 500 ? html.Substring(0, 500) : html;
                _logger.LogDebug("HTML preview: {Preview}", preview);
            }

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
        var prefCode = GetPrefectureCode(condition.Prefecture);

        // URL format: https://beauty.hotpepper.jp/pre13/
        // Pagination: https://beauty.hotpepper.jp/pre13/PN2.html
        var url = $"{_settings.BaseUrl}{prefCode}/";

        if (page > 1)
        {
            url += $"PN{page}.html";
        }

        return url;
    }

    private static string GetPrefectureCode(string? prefecture)
    {
        // Default to Tokyo if no prefecture specified
        if (string.IsNullOrEmpty(prefecture))
        {
            return "pre13";
        }

        // HotPepper Beauty prefecture codes
        // pre{XX} where XX is the JIS prefecture code
        return prefecture switch
        {
            "北海道" => "pre01",
            "青森県" => "pre02",
            "岩手県" => "pre03",
            "宮城県" => "pre04",
            "秋田県" => "pre05",
            "山形県" => "pre06",
            "福島県" => "pre07",
            "茨城県" => "pre08",
            "栃木県" => "pre09",
            "群馬県" => "pre10",
            "埼玉県" => "pre11",
            "千葉県" => "pre12",
            "東京都" => "pre13",
            "神奈川県" => "pre14",
            "新潟県" => "pre15",
            "富山県" => "pre16",
            "石川県" => "pre17",
            "福井県" => "pre18",
            "山梨県" => "pre19",
            "長野県" => "pre20",
            "岐阜県" => "pre21",
            "静岡県" => "pre22",
            "愛知県" => "pre23",
            "三重県" => "pre24",
            "滋賀県" => "pre25",
            "京都府" => "pre26",
            "大阪府" => "pre27",
            "兵庫県" => "pre28",
            "奈良県" => "pre29",
            "和歌山県" => "pre30",
            "鳥取県" => "pre31",
            "島根県" => "pre32",
            "岡山県" => "pre33",
            "広島県" => "pre34",
            "山口県" => "pre35",
            "徳島県" => "pre36",
            "香川県" => "pre37",
            "愛媛県" => "pre38",
            "高知県" => "pre39",
            "福岡県" => "pre40",
            "佐賀県" => "pre41",
            "長崎県" => "pre42",
            "熊本県" => "pre43",
            "大分県" => "pre44",
            "宮崎県" => "pre45",
            "鹿児島県" => "pre46",
            "沖縄県" => "pre47",
            _ => "pre13" // Default to Tokyo
        };
    }
}
