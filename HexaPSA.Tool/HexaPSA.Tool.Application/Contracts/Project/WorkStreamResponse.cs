namespace HexaPSA.Tool.Application.Contracts.Project
{
    public class WorkStreamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }

        public int Orders { get; set; }

    }

    public class WorkStreamMappingResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid ProjectId { get; set; }
    }


    public class WorkStreamUpdateResponse
    {
        public Guid Id { get; set; }
        public string? Activity { get; set; }
        public Guid RoleId { get; set; }
        public Guid WorkStreamActivityId { get; set; }
        public double Hours { get; set; }
        public int Week { get; set; }
        public string? Description { get; set; }
        public Guid? ParentId { get; set; }
    }

    public class WorkStreamTotalResponse
    {
        public Guid ProjectId { get; set; }
        public double Hours { get; set; }
        public int Week { get; set; }
    }
}
