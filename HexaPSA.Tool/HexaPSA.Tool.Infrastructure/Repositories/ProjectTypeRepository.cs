using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;
using Dapper;

namespace HexaPSA.Tool.Infrastructure.Repositories
{

    public class ProjectTypeRepository : IProjectTypeRepository
    {
        private readonly DapperDataContext _context;
        public ProjectTypeRepository(DapperDataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProjectType>> GetAllAsync()
        {
            var query = "SELECT * FROM ProjectType Where IsDestroyed =0";
            using (var connection = _context.CreateConnection())
            {
                var items = await connection.QueryAsync<ProjectType>(query);
                return items;

            }
        }
        public async Task<ProjectType> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM ProjectType Where Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<ProjectType>(query, new { id });
                return item;
            }
        }

        public async Task<ProjectType> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM ProjectType Where Name = @name";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<ProjectType>(query, new { name });
                return item;
            }
        }
         

        public async Task<ProjectType> AddAsync(ProjectType projectType)
        {
            var query = "INSERT INTO ProjectType (Name,CreatedDate,CreatedBy) OUTPUT inserted.Id VALUES (@Name, @CreatedDate, @CreatedBy)";
                       
            var parameters = new DynamicParameters();
            parameters.Add("Name", projectType.Name, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", projectType.CreatedBy, DbType.Guid);
            
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdProject = new ProjectType
                {
                    Id = id,
                    Name = projectType.Name
                    
                };
                return createdProject;
            }
        }

     

        public async Task<ProjectType> UpdateAsync(ProjectType projectType)
        {
            var query = "UPDATE ProjectType SET Name = @Name WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    projectType.Id,
                    projectType.Name,
                });
                return projectType;
            }
        }

        public async Task<ProjectType> DeleteAsync(Guid id)
        {
            var tech = await GetByIdAsync(id);
            if (tech != null)
            {
                var query = "UPDATE ProjectType SET IsDestroyed = 1 WHERE Id = @id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(query, new { id });
                }
            }
            var deletedResponce = new ProjectType { Id = id };
            return deletedResponce;
        }
    }
}
