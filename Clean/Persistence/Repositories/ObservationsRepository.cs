using ArchitectureConcepts.Clean.Core.Domain.Observations;
using ArchitectureConcepts.Clean.Core.Persistence.Database;

namespace ArchitectureConcepts.Clean.Core.Persistence.Repositories;

public class ObservationsRepository(ApplicationDbContext context) : IObservationsRepository
{
    public Task<Observation?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return context
            .Observations
            .FindAsync([id], cancellationToken)
            .AsTask();
    }

    public int Add(Observation observation)
    {
        _ = context.Observations.Add(observation);
        return observation.Id!.Value;
    }

    public void Update(Observation observation)
    {
        _ = context.Observations.Update(observation);
    }

    public void Delete(Observation observation)
    {
        _ = context.Observations.Update(observation);
    }
}