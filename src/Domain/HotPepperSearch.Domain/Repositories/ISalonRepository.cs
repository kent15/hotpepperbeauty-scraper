using HotPepperSearch.Domain.Entities;

namespace HotPepperSearch.Domain.Repositories;

public interface ISalonRepository
{
    Task<Salon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Salon>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Salon> AddAsync(Salon salon, CancellationToken cancellationToken = default);
    Task UpdateAsync(Salon salon, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
