using HotPepperSearch.Application.Common;
using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Services;

public class SalonSearchService : ISalonSearchService
{
    public Task<Result<IEnumerable<Salon>>> SearchSalonsByConditionAsync(SearchCondition condition, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
