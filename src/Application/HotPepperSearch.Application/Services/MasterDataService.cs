using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.Enums;

namespace HotPepperSearch.Application.Services;

public class MasterDataService : IMasterDataService
{
    public Task<IEnumerable<Genre>> GetGenresAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetPrefecturesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> GetCitiesByPrefectureAsync(string prefecture, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
