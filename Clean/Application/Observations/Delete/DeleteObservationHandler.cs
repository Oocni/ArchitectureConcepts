using ArchitectureConcepts.Clean.Core.Domain.Common;
using ArchitectureConcepts.Clean.Core.Domain.Observations;
using DotNext;
using MediatR;
using Unit = MediatR.Unit;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Delete;

public class DeleteObservationHandler(
    IObservationsRepository observationRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteObservationCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteObservationCommand request, CancellationToken cancellationToken)
    {
        var observation = await observationRepository.GetAsync(request.Id, cancellationToken);
        if (observation is null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Observation not found."));
        }
        if (observation.DeletedDate is not null || observation.DeletedBy is not null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Observation already deleted."));
        }

        observation.Delete(DateTime.UtcNow, request.DeletedBy);
        observationRepository.Delete(observation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}