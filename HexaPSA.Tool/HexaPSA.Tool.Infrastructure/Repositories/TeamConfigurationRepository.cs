using Dapper;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class TeamConfigurationRepository : ITeamConfigurationRepository
    {
        private readonly DapperDataContext _context;
        public TeamConfigurationRepository(DapperDataContext context)
        {
            _context = context;
        }

        public Task<TeamConfiguration> AddAsync(TeamConfiguration item)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamConfigResponse> AddAsync(TeamConfigResponse item)
        {
            var query = "INSERT INTO TeamConfiguration (UserId,ProjectId,RoleId,CreatedDate) OUTPUT inserted.Id VALUES (@UserId,@ProjectId, @RoleId,@CreatedDate)";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", item.UserId, DbType.Guid);
            parameters.Add("ProjectId", item.ProjectId, DbType.Guid);
            parameters.Add("RoleId", item.RoleId, DbType.Guid);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var teamId = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdTeam = new TeamConfigResponse
                {
                    Id = teamId,
                    UserId =item.UserId,
                    ProjectId = item.ProjectId,
                    RoleId = item.RoleId,
                };
                return createdTeam;
            }
        }

        public async Task<TeamConfiguration> DeleteAsync(Guid id)
        {
            var teamConfiguration = await GetTeamConfigurationByIdAsync(id);
            if (teamConfiguration != null)
            {
                var query = "UPDATE TeamConfiguration SET IsDestroyed = 1 WHERE Id = @Id";
                using (var connection = _context.CreateConnection())
                {
                    var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
                }
            }
            var deletedTeamConfiguration = new TeamConfiguration { Id = id };
            return deletedTeamConfiguration;
        }

        public Task<IEnumerable<TeamConfigurationResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TeamConfigResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamConfigResponse> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM TeamConfiguration WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var teamConfigration = await connection.QuerySingleOrDefaultAsync<TeamConfigResponse>(query, new { id });
                return teamConfigration;
            }
        }

        public async Task<List<TeamConfigurationResponse>> GetTeamConfigurationByIdAsync(Guid projectId)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @" 
                         SELECT
                         tc.Id,
                         tc.UserId,
                         tc.ProjectId,
                         tc.RoleId,
                         u.Id,
                         u.FullName,
                         r.Id,
                         r.Name,
                         p.Id,
                         p.Name
                         FROM TeamConfiguration tc
                         LEFT JOIN [User] u ON tc.UserId = u.Id
                         LEFT JOIN Project p ON tc.ProjectId = p.Id
                         LEFT JOIN Role r ON tc.RoleId = r.Id
                         WHERE tc.ProjectId = @ProjectId AND tc.IsDestroyed = 0";

                var parameters = new { ProjectId = projectId };

                var teamConfigurations = await connection.QueryAsync<TeamConfigurationResponse, UserTeamResponse, RoleTeamResponse, ProjectTeamResponse, TeamConfigurationResponse>(
                    sql,
                    (tcr, user, role, project) =>
                    {
                        tcr.user = user;
                        tcr.role = role;
                        tcr.projects = project;
                        return tcr;
                    },
                    splitOn: "Id,UserId,ProjectId,RoleId,Id,Id,Id",
                    param: parameters);

                return teamConfigurations.ToList();
            }
        }


        public  Task<TeamConfigurationResponse> UpdateAsync(TeamConfigurationResponse item)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamConfiguration> UpdateAsync(TeamConfiguration item)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamConfigResponse> UpdateAsync(TeamConfigResponse item)
        {
            var query = "UPDATE TeamConfiguration SET UserId = @UserId, ProjectId = @ProjectId, RoleId = @RoleId WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    item.UserId,
                    item.ProjectId,
                    item.RoleId,
                    item.Id
                });

                return item;
            }
        }

        Task<TeamConfigurationResponse> IReadRepository<TeamConfigurationResponse, Guid>.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
