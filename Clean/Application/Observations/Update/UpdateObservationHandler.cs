using ArchitectureConcepts.Clean.Core.Domain.Observations;
using ArchitectureConcepts.Common.Core.Domain.Common;
using ArchitectureConcepts.Common.Core.Domain.Observations;
using DotNext;
using MediatR;
using Unit = MediatR.Unit;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Update;

public class UpdateObservationHandler(
    IObservationsRepository observationRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateObservationCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateObservationCommand request, CancellationToken cancellationToken)
    {
        if (request.Name is null && request.Description is null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Name or description must be set."));
        }

        var observation = await observationRepository.GetAsync(request.Id, cancellationToken);
        if (observation is null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Observation not found."));
        }
        if (observation.DeletedDate is not null || observation.DeletedBy is not null)
        {
            return Result.FromException<Unit>(new InvalidOperationException("Deleted observations cannot be updated."));
        }

        if (request.Name is not null)
        {
            observation.UpdateName(request.Name);
        }
        if (request.Description is not null)
        {
            observation.UpdateDescription(request.Description);
        }

        observationRepository.Update(observation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}