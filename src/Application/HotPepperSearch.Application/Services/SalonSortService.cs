using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.Enums;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Services;

public class SalonSortService : ISalonSortService
{
    public IEnumerable<Salon> SortSalons(IEnumerable<Salon> salons, SortOption sortOption)
    {
        var salonList = salons.ToList();

        return sortOption.Field switch
        {
            SortField.Rating => SortByRating(salonList, sortOption.Order),
            SortField.ReviewCount => SortByReviewCount(salonList, sortOption.Order),
            SortField.Price => SortByPrice(salonList, sortOption.Order),
            _ => salonList
        };
    }

    private static IEnumerable<Salon> SortByRating(List<Salon> salons, SortOrder order)
    {
        return order == SortOrder.Ascending
            ? salons.OrderBy(s => s.Rating ?? 0)
            : salons.OrderByDescending(s => s.Rating ?? 0);
    }

    private static IEnumerable<Salon> SortByReviewCount(List<Salon> salons, SortOrder order)
    {
        return order == SortOrder.Ascending
            ? salons.OrderBy(s => s.ReviewCount)
            : salons.OrderByDescending(s => s.ReviewCount);
    }

    private static IEnumerable<Salon> SortByPrice(List<Salon> salons, SortOrder order)
    {
        return order == SortOrder.Ascending
            ? salons.OrderBy(s => ExtractPriceValue(s.PriceRange))
            : salons.OrderByDescending(s => ExtractPriceValue(s.PriceRange));
    }

    private static int ExtractPriceValue(string? priceRange)
    {
        if (string.IsNullOrEmpty(priceRange))
            return 0;

        var numericChars = new string(priceRange.Where(char.IsDigit).ToArray());
        return int.TryParse(numericChars, out var price) ? price : 0;
    }
}
