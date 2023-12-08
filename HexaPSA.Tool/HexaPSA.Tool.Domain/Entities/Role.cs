namespace HexaPSA.Tool.Domain.Entities;

public class Role : EntityBase<Guid>
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<CapacityUtilization> CapacityUtilizations { get; set; } = new List<CapacityUtilization>();

    public virtual ICollection<ProjectUserEstimationMapping> ProjectUserEstimationMappings { get; set; } = new List<ProjectUserEstimationMapping>();

    public virtual ICollection<ResourceRate> ResourceRate { get; set; } = new List<ResourceRate>();

    public virtual ICollection<TeamConfiguration> TeamConfigurations { get; set; } = new List<TeamConfiguration>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<WorkStreamActivity> WorkStreamActivities { get; set; } = new List<WorkStreamActivity>();
}
