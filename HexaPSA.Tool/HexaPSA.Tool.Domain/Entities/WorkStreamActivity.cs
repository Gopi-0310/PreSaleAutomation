namespace HexaPSA.Tool.Domain.Entities;

public class WorkStreamActivity: EntityBase<Guid>
{
    public string? Activity { get; set; }

    public Guid? RoleId { get; set; }

    public Guid WorkStreamActivityId { get; set; }
  
    public double? Hours { get; set; }

    public int? Week { get; set; }
    public string? Description { get; set; }
    public virtual Guid? ParentId { get; set; } = Guid.Empty;
    public virtual Role? Role { get; set; }
    public virtual WorkStream? WorkStreams { get; set; }
    
}
//added