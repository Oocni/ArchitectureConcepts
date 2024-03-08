namespace Domains.Observations;

public interface IObservationsRepository
{
    /// <summary>
    /// Get an observation by its id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns the observation or null if not find</returns>
    Task<Observation?> GetAsync(int id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get all observations
    /// </summary>
    /// <param name="id">Id to begin the search from</param>
    /// <param name="limit">Nb of observations to return</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Returns the observations</returns>
    Task<IEnumerable<Observation>> GetAllAsync(int? id, int limit, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Add an observation
    /// </summary>
    /// <param name="observation"></param>
    /// <returns>Returns the id of the observation</returns>
    int Add(Observation observation);
    
    /// <summary>
    /// Update an observation
    /// </summary>
    /// <param name="observation"></param>
    void Update(Observation observation);
    
    /// <summary>
    /// Delete an observation
    /// </summary>
    /// <param name="observation"></param>
    void Delete(Observation observation);
}