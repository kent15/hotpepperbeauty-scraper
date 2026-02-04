using HotPepperSearch.Domain.Enums;

namespace HotPepperSearch.Domain.Entities;

public class Salon
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public string Area { get; set; } = string.Empty;
    public string NearestStation { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public int ReviewCount { get; set; }
    public string? PriceRange { get; set; }
    public string? Url { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
