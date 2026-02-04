using HotPepperSearch.Application.Common;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Interfaces;

public interface ISalonSearchService
{
    Task<Result<IEnumerable<Salon>>> SearchSalonsByConditionAsync(SearchCondition condition, CancellationToken cancellationToken = default);
}
