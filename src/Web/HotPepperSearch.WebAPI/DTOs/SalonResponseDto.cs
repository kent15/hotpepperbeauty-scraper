namespace HotPepperSearch.WebAPI.DTOs;

public class SalonResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string NearestStation { get; set; } = string.Empty;
    public decimal? Rating { get; set; }
    public int ReviewCount { get; set; }
    public string? PriceRange { get; set; }
    public string? Url { get; set; }
}
