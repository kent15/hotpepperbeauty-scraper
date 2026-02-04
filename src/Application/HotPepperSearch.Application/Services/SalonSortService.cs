using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Services;

public class SalonSortService : ISalonSortService
{
    public IEnumerable<Salon> SortSalons(IEnumerable<Salon> salons, SortOption sortOption)
    {
        throw new NotImplementedException();
    }
}
