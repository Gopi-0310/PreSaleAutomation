namespace HexaPSA.Tool.Domain.Entities;

public class ProjectUserEstimationMapping: EntityBase<Guid>
{
    public Guid? UserId { get; set; }

    public Guid? RoleId { get; set; }

    public Guid? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Role? Role { get; set; }

    public virtual User? User { get; set; }
    public string CurrentStatus { get; set; }
}
