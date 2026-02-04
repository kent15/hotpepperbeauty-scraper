using HotPepperSearch.Domain.Enums;

namespace HotPepperSearch.Domain.ValueObjects;

public class SortOption
{
    public SortField Field { get; set; }
    public SortOrder Order { get; set; }
}
