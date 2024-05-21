using DotNext;
using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Delete;

public record DeleteObservationCommand(int Id, string DeletedBy) : IRequest<Result<Unit>>;