using ArchitectureConcepts.Common.Core.Domain.Observations;
using ArchitectureConcepts.Common.External.Persistance.Database;
using ArchitectureConcepts.Onion.Core.Domain.Observations;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureConcepts.Onion.External.Persistence.Repositories;

public class ObservationsRepository : IObservationsRepository
{
    private readonly ApplicationDbContext _context;

    public ObservationsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Observation?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return _context
            .Observations
            .FindAsync([id], cancellationToken)
            .AsTask();
    }

    public async Task<IReadOnlyCollection<Observation>> GetAllAsync(int? id, int limit = 10, CancellationToken cancellationToken = default)
    {
        var query = _context
            .Observations
            .AsQueryable();

        if (id is not null)
        {
            query = query.Where(o => o.Id >= id);
        }

        if (limit is <= 0 or > 100)
        {
            limit = 10;
        }

        return await query
            .AsNoTracking()
            .OrderBy(o => o.Id)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }

    public int Add(Observation observation)
    {
        _ = _context.Observations.Add(observation);
        return observation.Id!.Value;
    }

    public void Update(Observation observation)
    {
        _ = _context.Observations.Update(observation);
    }

    public void Delete(Observation observation)
    {
        _ = _context.Observations.Update(observation);
    }
}