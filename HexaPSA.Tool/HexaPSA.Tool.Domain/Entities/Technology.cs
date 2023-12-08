namespace HexaPSA.Tool.Domain.Entities;

public class Technology : EntityBase<Guid>
{
    public string? Name { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
