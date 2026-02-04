namespace HotPepperSearch.Infrastructure.Scraping.Configuration;

public class ScrapingSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public int MinDelay { get; set; }
    public int MaxDelay { get; set; }
    public int PageDelay { get; set; }
    public int MaxRetries { get; set; }
    public int MaxPagesPerSearch { get; set; }
    public string UserAgent { get; set; } = string.Empty;
}
