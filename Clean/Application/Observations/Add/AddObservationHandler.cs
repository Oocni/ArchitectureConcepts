using ArchitectureConcepts.Clean.Core.Domain.Observations;
using ArchitectureConcepts.Common.Core.Domain.Common;
using ArchitectureConcepts.Common.Core.Domain.Observations;
using DotNext;
using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Add;

public class AddObservationHandler(
    IObservationsRepository observationRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddObservationCommand, Result<int>>
{
    public async Task<Result<int>> Handle(AddObservationCommand request, CancellationToken cancellationToken)
    {
        var observation = (Observation)request;
        var observationId = observationRepository.Add(observation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return observationId;
    }
}