using System.Security.Cryptography.X509Certificates;

namespace HexaPSA.Tool.Application.Contracts.Project
{
    public sealed record ResourceRateCardResponse
    {
        public Guid? Id { get; set; }
        public RoleResponse? role {get; set;}
        public Guid? ProjectId { get; set; }
        public decimal? Rate { get; set; }
    }

    public sealed record ResourceRateCardResponseRoleId(Guid? Id, Guid?  RoleId, Guid? ProjectId = null, decimal? Rate = null);
    
}
