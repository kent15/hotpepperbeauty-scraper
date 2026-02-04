using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.ValueObjects;

namespace HotPepperSearch.Application.Interfaces;

public interface ISalonSortService
{
    IEnumerable<Salon> SortSalons(IEnumerable<Salon> salons, SortOption sortOption);
}
