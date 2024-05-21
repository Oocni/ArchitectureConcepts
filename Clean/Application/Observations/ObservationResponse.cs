using ArchitectureConcepts.Clean.Core.Domain.Observations;

namespace ArchitectureConcepts.Clean.Core.Application.Observations;

public record ObservationResponse(
    int Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    string CreatedBy,
    DateTime? DeletedAt,
    string? DeletedBy)
{
    public static implicit operator ObservationResponse?(Observation? observation)
    {
        if (observation?.Id == null)
        {
            return null;
        }
        
        return new ObservationResponse(
            observation.Id.Value,
            observation.Name,
            observation.Description,
            observation.CreatedDate,
            observation.CreatedBy,
            observation.DeletedDate,
            observation.DeletedBy);
    }
}
