namespace Domains.Observations;

public class Observation(
    string name,
    DateTime createdDate,
    string createdBy,
    string? description = null)
{
    public int? Id { get; init; }
    public DateTime CreatedDate { get; init; } = createdDate;
    public string CreatedBy { get; init; } = createdBy;

    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;
    public DateTime? DeletedDate { get; private set; }
    public string? DeletedBy { get; private set; }
    
    public void UpdateName(string name)
    {
        Name = name;
    }
    
    public void UpdateDescription(string description)
    {
        Description = description;
    }
    
    public void Delete(DateTime deletedDate, string deletedBy)
    {
        DeletedDate = deletedDate;
        DeletedBy = deletedBy;
    }
}