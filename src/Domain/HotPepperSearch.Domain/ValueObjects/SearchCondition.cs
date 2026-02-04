using HotPepperSearch.Domain.Enums;

namespace HotPepperSearch.Domain.ValueObjects;

public class SearchCondition
{
    public Genre? Genre { get; set; }
    public string? Prefecture { get; set; }
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinRating { get; set; }
}
