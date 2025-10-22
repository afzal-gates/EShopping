namespace Analytics.Core.Entities;

public abstract class EntityBase
{
    public int Id { get; set; }
    public string CreatedBy { get; set; } = "system";
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
