using Dapper;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class TechnologyRepository :ITechnologyRepository
    {
        private readonly DapperDataContext _context;
        public TechnologyRepository(DapperDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technology>> GetAllAsync()
        {
            var query = "SELECT * FROM Technology Where IsDestroyed = 0";
            using (var connection = _context.CreateConnection())
            {
                var items = await connection.QueryAsync<Technology>(query);
                return items;

            }
        }

        public async Task<Technology> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Technology Where Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<Technology>(query, new { id });
                return item;
            }
        }

        public async Task<Technology> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM Technology Where Name = @name";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<Technology>(query, new { name });
                return item;
            }
        }


        public async Task<Technology> AddAsync(Technology technology)
        {
            var query = "INSERT INTO Technology (Name,CreatedDate,CreatedBy) OUTPUT inserted.Id VALUES (@Name, @CreatedDate, @CreatedBy)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", technology.Name, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", technology.CreatedBy, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdTechnology = new Technology
                {
                    Id = id,
                    Name = technology.Name,
                    

                };
                return createdTechnology;
            }
        }

        public async Task<Technology> UpdateAsync(Technology technology)
        {
            var query = "UPDATE Technology SET Name = @Name WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new
                {
                    technology.Id,
                    technology.Name,
                });
                return technology;
            }
        }

        public async Task<Technology> DeleteAsync(Guid id)
        {
            var tech = await GetByIdAsync(id);
            if (tech != null)
            {
                var query = "UPDATE Technology SET IsDestroyed = 1 WHERE Id = @id";
                using (var connection = _context.CreateConnection())
                {
                    var changesRow = await connection.ExecuteAsync(query, new { id });
                }
            }
            var deletedResponce = new Technology { Id = id };
            return deletedResponce;
        }

    }




}

