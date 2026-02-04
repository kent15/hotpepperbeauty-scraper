using AngleSharp;
using AngleSharp.Dom;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.Enums;
using HotPepperSearch.Infrastructure.Scraping.Interfaces;

namespace HotPepperSearch.Infrastructure.Scraping.Services;

public class AngleSharpHtmlParserService : IHtmlParserService
{
    public async Task<IEnumerable<Salon>> ParseSalonListAsync(string html, CancellationToken cancellationToken = default)
    {
        var config = Configuration.Default;
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync(req => req.Content(html), cancellationToken);

        var salons = new List<Salon>();
        var salonElements = document.QuerySelectorAll(".slnCassette");

        foreach (var element in salonElements)
        {
            var salon = ParseSalonElement(element);
            if (salon != null)
            {
                salons.Add(salon);
            }
        }

        return salons;
    }

    private static Salon? ParseSalonElement(IElement element)
    {
        var nameElement = element.QuerySelector(".slnName a");
        if (nameElement == null) return null;

        var name = nameElement.TextContent.Trim();
        var url = nameElement.GetAttribute("href");

        var areaElement = element.QuerySelector(".slnCassetteInfo .slnCassetteInfoItem:nth-child(1)");
        var area = areaElement?.TextContent.Trim() ?? string.Empty;

        var stationElement = element.QuerySelector(".slnCassetteInfo .slnCassetteInfoItem:nth-child(2)");
        var nearestStation = stationElement?.TextContent.Trim() ?? string.Empty;

        var ratingElement = element.QuerySelector(".reviewScore");
        decimal? rating = null;
        if (ratingElement != null && decimal.TryParse(ratingElement.TextContent.Trim(), out var parsedRating))
        {
            rating = parsedRating;
        }

        var reviewCountElement = element.QuerySelector(".reviewNum");
        var reviewCount = 0;
        if (reviewCountElement != null)
        {
            var reviewText = reviewCountElement.TextContent.Trim();
            var numericText = new string(reviewText.Where(char.IsDigit).ToArray());
            int.TryParse(numericText, out reviewCount);
        }

        var priceElement = element.QuerySelector(".slnCassettePrice");
        var priceRange = priceElement?.TextContent.Trim();

        return new Salon
        {
            Id = Guid.NewGuid(),
            Name = name,
            Genre = Genre.HairSalon,
            Area = area,
            NearestStation = nearestStation,
            Rating = rating,
            ReviewCount = reviewCount,
            PriceRange = priceRange,
            Url = url,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
