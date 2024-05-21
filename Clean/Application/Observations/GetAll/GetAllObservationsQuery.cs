using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.GetAll;

public record GetAllObservationsQuery(int? Id, int? Limit) : IRequest<IEnumerable<ObservationResponse>>;
