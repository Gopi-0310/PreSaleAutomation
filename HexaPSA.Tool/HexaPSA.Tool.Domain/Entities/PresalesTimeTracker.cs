namespace HexaPSA.Tool.Domain.Entities;

public  class PresalesTimeTracker: EntityBase<Guid>
{
    public Guid? UserId { get; set; }

    public string? Activity { get; set; }

    public string? Description { get; set; }

    public double? Hours { get; set; }

    public Guid? ProjectId { get; set; }

    public DateTime? ActivityDate { get; set; }

    public virtual Project? Project { get; set; }

    public virtual User? User { get; set; }
}
