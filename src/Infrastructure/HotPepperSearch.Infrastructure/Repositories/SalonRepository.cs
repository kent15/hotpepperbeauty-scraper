using HotPepperSearch.Domain.Entities;
using HotPepperSearch.Domain.Repositories;
using HotPepperSearch.Infrastructure.Data;

namespace HotPepperSearch.Infrastructure.Repositories;

public class SalonRepository : ISalonRepository
{
    private readonly ApplicationDbContext _context;

    public SalonRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Salon?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Salon>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Salon> AddAsync(Salon salon, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Salon salon, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
