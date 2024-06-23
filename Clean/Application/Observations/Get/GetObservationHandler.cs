using ArchitectureConcepts.Clean.Core.Domain.Observations;
using ArchitectureConcepts.Common.Core.Domain.Observations;
using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Get;

public class GetObservationHandler(IObservationsRepository observationRepository)
    : IRequestHandler<GetObservationQuery, ObservationResponse?>
{
    public async Task<ObservationResponse?> Handle(GetObservationQuery query, CancellationToken cancellationToken)
    {
        return await observationRepository.GetAsync(query.Id, cancellationToken);
    }
}