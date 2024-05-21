namespace ArchitectureConcepts.Clean.Core.Domain.Observations;

/// <summary>
/// Observation entity
/// </summary>
/// <param name="name"></param>
/// <param name="createdDate"></param>
/// <param name="createdBy"></param>
/// <param name="description">Can be null</param>
public class Observation(
    string name,
    DateTime createdDate,
    string createdBy,
    string? description = null)
{
    /// <summary>
    /// Id of the observation
    /// </summary>
    public int? Id { get; init; }
    
    /// <summary>
    /// Created date of the observation
    /// </summary>
    public DateTime CreatedDate { get; init; } = createdDate;
    
    /// <summary>
    /// Created by of the observation
    /// </summary>
    public string CreatedBy { get; init; } = createdBy;

    /// <summary>
    /// Name of the observation
    /// </summary>
    public string Name { get; private set; } = name;
    
    /// <summary>
    /// Description of the observation
    /// </summary>
    public string? Description { get; private set; } = description;
    
    /// <summary>
    /// Deleted date of the observation
    /// </summary>
    public DateTime? DeletedDate { get; private set; }
    
    /// <summary>
    /// Deleted by of the observation
    /// </summary>
    public string? DeletedBy { get; private set; }
    
    /// <summary>
    /// Update the name of the observation
    /// </summary>
    /// <param name="name"></param>
    public void UpdateName(string name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Update the description of the observation
    /// </summary>
    /// <param name="description"></param>
    public void UpdateDescription(string description)
    {
        Description = description;
    }
    
    /// <summary>
    /// Set the deleted date and deleted by of the observation
    /// </summary>
    /// <param name="deletedDate"></param>
    /// <param name="deletedBy"></param>
    public void Delete(DateTime deletedDate, string deletedBy)
    {
        DeletedDate = deletedDate;
        DeletedBy = deletedBy;
    }
}