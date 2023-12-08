namespace HexaPSA.Tool.Application.Contracts.Project
{
    public sealed record CapacityUtilizationResponse(Guid? Id ,Guid? ProjectId = null,Guid? RoleId = null, double Hours = 0.0, decimal Rate = 0.0M, string? Location = null)
    {
    }
}
