using Domain.Observations;

namespace Application.Observations;

public record CreateObservationParameters(string Name, string CreatedBy, string? Description = null)
{
    public static explicit operator Observation(CreateObservationParameters parameters) =>
        new Observation(parameters.Name, DateTime.UtcNow,  parameters.CreatedBy, parameters.Description);
}