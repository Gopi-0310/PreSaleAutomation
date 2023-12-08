using Dapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Interfaces.Common;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;



namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class CapacityUtilizationRepository : ICapacityUtilizationRepository
    {
        private readonly DapperDataContext _context;
        public CapacityUtilizationRepository(DapperDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CapacityUtilization>> GetAllAsync()
        {
            var query = @"SELECT c.Id,c.ProjectId,c.RoleId,c.Hours,c.Location,r.RoleId ,r.Code, r.Name, rr.Rate
                FROM CapacityUtilization c 
                INNER JOIN Role r ON c.RoleId = r.RoleId
                LEFT JOIN ResourceRate rr ON rr.RoleId = c.RoleId
                WHERE rr.ProjectId = @ProjectId AND c.IsDestroyed = 0";
            using (var connection = _context.CreateConnection())
            {
                var capacityUtilizations = await connection.QueryAsync<CapacityUtilization, Role, CapacityUtilization>(
                    query,
                    (cu, role) =>
                    {
                        cu.Role = role;
                        //cu.ResourceRate = resourceRate;
                        return cu;
                    },
                    splitOn: "ProjectId,RoleId"
                );
                return capacityUtilizations;
            }
        }

        public async Task<List<CapacityMappingResponse>> GetCapacityUtilizationByIdAsync(Guid id)
        {
            var query = "SELECT c.Id,c.ProjectId ,c.RoleId,c.Hours,c.Location ,r.Id ,r.Code,r.Name ,rr.Id ,rr.RoleId ,rr.ProjectId ,rr.Rate  FROM CapacityUtilization c JOIN Role r ON c.RoleId = r.Id JOIN ResourceRate rr ON r.Id = rr.RoleId WHERE c.ProjectId = @id AND rr.ProjectId = @id AND c.IsDestroyed = 0;";

            using (var connection = _context.CreateConnection())
            {
                var capacityUtilizations = await connection.QueryAsync<CapacityMappingResponse, CapacityRoleResponse, CapacityResourceRateResponse, CapacityMappingResponse>(
                    query,
                    (cu, role, resorce) =>
                    {
                        cu.Role = role;
                        cu.ResourceRate = resorce;
                        cu.Hours = cu.Hours;
                        cu.Location = cu.Location;

                        //  cu.ResourceRate.ResourceId = resorce.ResourceId;
                        //  cu.ResourceRate.Id = resorce.Id;
                        return cu;
                    },
                    new { id }, // Properly binding the parameter
                    splitOn: "Id,ProjectId,RoleId,Id,Id"
                );
                return (List<CapacityMappingResponse>)capacityUtilizations;
            }
        }
        public async Task<CapacityUtilization> AddAsync(CapacityUtilization capacity)
        {
            var query = "INSERT INTO CapacityUtilization (RoleId,ProjectId,Hours,Location,CreatedDate,CreatedBy) OUTPUT inserted.Id VALUES (@RoleId,@ProjectId,@Hours,@Location, @CreatedDate, @CreatedBy)";
            var parameters = new DynamicParameters();
            parameters.Add("RoleId", capacity.RoleId, DbType.Guid);
            parameters.Add("ProjectId", capacity.ProjectId, DbType.Guid);
            parameters.Add("Hours", capacity.Hours, DbType.Double);
            parameters.Add("Location", capacity.Location, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", capacity.CreatedBy, DbType.Guid);
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var addCapacityUtilzation = new CapacityUtilization
                {
                    Id = id,
                    RoleId = capacity.RoleId,
                    ProjectId = capacity.ProjectId,
                    Hours = capacity.Hours,
                    Location = capacity.Location

                };
                return addCapacityUtilzation;
            }
        }
        public async Task<CapacityUtilization> UpdateAsync(CapacityUtilization capacity)
        {
            var query = "UPDATE CapacityUtilization SET ProjectId = @ProjectId, RoleId = @RoleId, Hours = @Hours, Location = @Location WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, capacity);
                return capacity;
            }
        }
        public async Task<CapacityUtilization> DeleteAsync(Guid id)
        {
            var capacityUtilization = await GetByIdAsync(id);
            if (capacityUtilization != null)
            {
                var query = "UPDATE CapacityUtilization SET IsDestroyed = 1 WHERE Id = @id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(query, new { id });
                }
            }
            var deletedResponce = new CapacityUtilization { Id = id };
            return deletedResponce;
        }
        public async Task<CapacityUtilization> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM CapacityUtilization WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var capacityUtilization = await connection.QuerySingleOrDefaultAsync<CapacityUtilization>(query, new { id });
                return capacityUtilization;
            }
        }

       
    }
}