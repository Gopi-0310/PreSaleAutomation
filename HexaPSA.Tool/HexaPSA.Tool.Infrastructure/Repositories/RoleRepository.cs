using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;
using Dapper;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DapperDataContext _context;

        public RoleRepository(DapperDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            var query = "SELECT * FROM Role WHERE IsDestroyed = 0";
            using (var connection = _context.CreateConnection())
            {
                var roles = await connection.QueryAsync<Role>(query);
                return roles;
            }
        }

        public async Task<Role> GetByIdAsync(Guid roleId)
        {
            var query = "SELECT * FROM Role WHERE Id = @roleId";
            using (var connection = _context.CreateConnection())
            {
                var role = await connection.QuerySingleOrDefaultAsync<Role>(query, new { roleId });
                return role;
            }
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM Role WHERE Name = @name";
            using (var connection = _context.CreateConnection())
            {
                var role = await connection.QuerySingleOrDefaultAsync<Role>(query, new { name });
                return role;
            }
        }

        public async Task<Role> AddAsync(Role role)
        {
            var query = "INSERT INTO Role (Name,Code, CreatedDate, CreatedBy) OUTPUT inserted.Id VALUES (@Name,@Code, @CreatedDate, @CreatedBy)";

            var parameters = new DynamicParameters(); 
            parameters.Add("Name", role.Name, DbType.String);
            parameters.Add("Code", role.Code, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", role.CreatedBy, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var roleId = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdRole = new Role
                {
                    Id = roleId,
                    Name = role.Name,
                    Code = role.Code
                    // Add other properties here if needed
                };
                return createdRole;
            }
        }

        public async Task<Role> UpdateAsync(Role role)
        {
            var query = "UPDATE Role SET Name = @Name, Code = @Code WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    role.Id,
                    role.Name,
                    role.Code
                });
                return role;
            }
        }

      

        public async Task<Role> DeleteAsync(Guid id)
        {
            var role= await GetByIdAsync(id);
            if (role != null)
            {
                var query = "UPDATE Role SET IsDestroyed = 1 WHERE Id = @Id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(query, new { id });
                }
            }
            var deletedRole = new Role { Id = id };
            return deletedRole;
        }



    }
}
