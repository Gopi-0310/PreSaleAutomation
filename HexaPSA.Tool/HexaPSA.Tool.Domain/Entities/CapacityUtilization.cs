namespace HexaPSA.Tool.Domain.Entities;

public  class CapacityUtilization: EntityBase<Guid>
{
    public Guid? ProjectId { get; set; }

    public Guid? RoleId { get; set; }

    public double? Hours { get; set; }

    public decimal? Rate { get; set; }
    public string? Location { get; set; }
    

    public virtual Project? Project { get; set; }

    public virtual Role? Role { get; set; }
    
    public ResourceRate? ResourceRate { get; set; }
   

}
