namespace HotPepperSearch.WebAPI.DTOs;

public class SortRequestDto
{
    public string Field { get; set; } = string.Empty;
    public string Order { get; set; } = "Descending";
}
