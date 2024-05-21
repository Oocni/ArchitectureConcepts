using DotNext;
using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Update;

public record UpdateObservationCommand(int Id, string? Name=null, string? Description = null) : IRequest<Result<Unit>>;