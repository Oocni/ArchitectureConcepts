namespace Domains.Observations;

public interface IObservationsRepository
{
    Task<Observation?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Observation>> GetAllAsync(int? id, int limit, CancellationToken cancellationToken = default);
    
    int Add(Observation observation);
    void Update(Observation observation);
    void Delete(Observation observation);
}