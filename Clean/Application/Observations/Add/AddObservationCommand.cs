using ArchitectureConcepts.Clean.Core.Domain.Observations;
using MediatR;

namespace ArchitectureConcepts.Clean.Core.Application.Observations.Add;

public record AddObservationCommand(string Name, string CreatedBy, string? Description = null) : IRequest<int>
{
    public static explicit operator Observation(AddObservationCommand command) =>
        new Observation(command.Name, DateTime.UtcNow,  command.CreatedBy, command.Description);
}