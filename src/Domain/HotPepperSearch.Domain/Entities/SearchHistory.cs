namespace HotPepperSearch.Domain.Entities;

public class SearchHistory
{
    public Guid Id { get; set; }
    public string SearchConditionJson { get; set; } = string.Empty;
    public int ResultCount { get; set; }
    public DateTime SearchedAt { get; set; }
}
