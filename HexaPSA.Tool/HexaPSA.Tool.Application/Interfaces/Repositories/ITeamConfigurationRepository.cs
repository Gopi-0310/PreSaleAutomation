using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Application.Interfaces.Repositories
{
    public interface ITeamConfigurationReadRepository : IReadRepository<TeamConfigurationResponse, Guid>
    {
        Task<List<TeamConfigurationResponse>> GetTeamConfigurationByIdAsync(Guid id);
        Task<TeamConfigResponse> GetById(Guid id);
        Task<TeamConfigResponse> GetByIdAsync(Guid id);
    }

    public interface ITeamConfigurationWriteRepository : IWriteRepository<TeamConfiguration, Guid>
    {
       
    }

    public interface ITeamConfigurationRepository : ITeamConfigurationReadRepository, ITeamConfigurationWriteRepository
    {
        Task<TeamConfigResponse> AddAsync(TeamConfigResponse item);

        Task<TeamConfigResponse> UpdateAsync(TeamConfigResponse item);

        
    }
}
