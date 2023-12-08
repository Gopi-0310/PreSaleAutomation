namespace HexaPSA.Tool.Domain.Entities;

public class ResourceRate: EntityBase<Guid>
{
    //public Guid? Id { get; set; }
    public Guid? RoleId { get; set; }

    public Guid? ProjectId { get; set; } 

    public decimal? Rate { get; set; }

    public virtual Project? Project { get; set; } 

    public virtual Role? Role { get; set; }


}
