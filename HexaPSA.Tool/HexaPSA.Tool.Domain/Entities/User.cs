namespace HexaPSA.Tool.Domain.Entities;

public class User: EntityBase<Guid>
{
    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public Guid? RoleId { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<PresalesTimeTracker> PresalesTimeTrackers { get; set; } = new List<PresalesTimeTracker>();

    public virtual ICollection<ProjectUserEstimationMapping> ProjectUserEstimationMappings { get; set; } = new List<ProjectUserEstimationMapping>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<TeamConfiguration> TeamConfigurations { get; set; } = new List<TeamConfiguration>();
}
