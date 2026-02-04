namespace HotPepperSearch.Infrastructure.Scraping.Interfaces;

public interface IDelayService
{
    Task DelayAsync(CancellationToken cancellationToken = default);
}
