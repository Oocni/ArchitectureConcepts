using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Get;

public record GetObservationQuery(int Id) : IRequest<ObservationResponse?>;
