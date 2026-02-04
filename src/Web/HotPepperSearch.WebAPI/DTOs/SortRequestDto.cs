namespace HotPepperSearch.WebAPI.DTOs;

public class SortRequestDto
{
    public string Field { get; set; } = "Rating";
    public string Order { get; set; } = "Descending";
    public List<SalonResponseDto> Salons { get; set; } = new();
}
