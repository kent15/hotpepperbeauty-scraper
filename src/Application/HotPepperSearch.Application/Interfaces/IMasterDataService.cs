using HotPepperSearch.Domain.Enums;

namespace HotPepperSearch.Application.Interfaces;

public interface IMasterDataService
{
    Task<IEnumerable<Genre>> GetGenresAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetPrefecturesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetCitiesByPrefectureAsync(string prefecture, CancellationToken cancellationToken = default);
}
