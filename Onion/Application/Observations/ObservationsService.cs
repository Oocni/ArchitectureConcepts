using Domain.Common;
using Domain.Observations;
using DotNext;

namespace Application.Observations;

public class ObservationsService(
    IObservationsRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<ObservationResponse?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await repository.GetAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<ObservationResponse>> GetAllAsync(int? id, int limit, CancellationToken cancellationToken = default)
    {
        var observations = await repository.GetAllAsync(id, limit, cancellationToken);
        return observations.Select(o => (ObservationResponse)o!);
    }

    public async Task<Result<int>> AddAsync(CreateObservationParameters parameters, CancellationToken cancellationToken = default)
    {
        var observation = (Observation)parameters;
        var observationId = repository.Add(observation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return observationId;
    }

    public async Task<Result<Unit>> UpdateAsync(int id, string? name, string? description, CancellationToken cancellationToken = default)
    {
        if (name is null && description is null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Name or description must be set."));
        }

        var observation = await repository.GetAsync(id, cancellationToken);
        if (observation is null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Observation not found."));
        }
        if (observation.DeletedDate is not null || observation.DeletedBy is not null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Deleted observations cannot be updated."));
        }

        if (name is not null)
        {
            observation.UpdateName(name);
        }
        if (description is not null)
        {
            observation.UpdateDescription(description);
        }

        repository.Update(observation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    public async Task<Result<Unit>> DeleteAsync(int id, string deletedBy, CancellationToken cancellationToken = default)
    {
        var observation = await repository.GetAsync(id, cancellationToken);
        if (observation is null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Observation not found."));
        }
        if (observation.DeletedDate is not null || observation.DeletedBy is not null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Observation already deleted."));
        }

        observation.Delete(DateTime.UtcNow, deletedBy);
        repository.Delete(observation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}