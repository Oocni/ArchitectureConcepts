using ArchitectureConcepts.Clean.Core.Domain.Common;
using ArchitectureConcepts.Clean.Core.Domain.Observations;
using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Add;

public class AddObservationHandler(
    IObservationsRepository observationRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddObservationCommand, int>
{
    public async Task<int> Handle(AddObservationCommand request, CancellationToken cancellationToken)
    {
        var observation = (Observation)request;
        var observationId = observationRepository.Add(observation);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return observationId;
    }
}