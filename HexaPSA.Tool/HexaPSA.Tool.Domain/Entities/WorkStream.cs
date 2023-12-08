namespace HexaPSA.Tool.Domain.Entities;

public class WorkStream : EntityBase<Guid>
{   
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid ProjectId { get; set; }
    public virtual Project? Project { get; set; }
    public int? Orders { get; set; }
    public virtual ICollection<WorkStreamActivity> WorkStreamActivity { get; set; } = new List<WorkStreamActivity>();

}
