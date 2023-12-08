using Dapper;
using HexaPSA.Tool.Application.Contracts.Project;
using HexaPSA.Tool.Application.Contracts;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System;
using System.Data;
using System.Threading.Tasks;
using HexaPSA.Tool.Application.Interfaces.Common;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDataContext _context;

        public UserRepository(DapperDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AllUserResponse>> GetAllAsync()
        {
            var query = @" SELECT
               u.Id,
               u.UserName,
               u.FullName,
               u.RoleId,
               u.EMail,
               r.Id,
               r.Name,
               r.Code 
               FROM [User] u
               Inner JOIN Role r ON u.RoleId = r.Id
               WHERE u.IsDestroyed = 0";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<AllUserResponse, RoleResponse, AllUserResponse>(
                   query,
                   (user, role) =>
                   {
                       user.Role = role;
                       return user;
                   },
                   splitOn: "Id,RoleId,Id"
               );
                return (List<AllUserResponse>)users;
            }
        }


        public async Task<User> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM [User] WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<User>(query, new { id });
                return item;
            }
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM User WHERE UserName = @name";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<User>(query, new { name });
                return item;
            }
        }

        public async Task<User> AddAsync(User user)
        {
            var query = "INSERT INTO [User] (UserName,  FullName, RoleId, EMail, CreatedDate, CreatedBy) " +
                        "OUTPUT inserted.Id " +
                        "VALUES (@UserName,  @FullName, @RoleId, @EMail, @CreatedDate, @CreatedBy)";

            var parameters = new DynamicParameters();
            parameters.Add("UserName", user.UserName, DbType.String);
            parameters.Add("FullName", user.FullName, DbType.String);
            parameters.Add("RoleId", user.RoleId, DbType.Guid);
            parameters.Add("EMail", user.Email, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", user.CreatedBy, DbType.Guid);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdUser = new User
                {
                    Id = id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    RoleId = user.RoleId,
                    Email = user.Email
                };
                return createdUser;
            }
        }

        public Task<User> UpdateAsync(User item) => throw new NotImplementedException();

        public Task<User> DeleteAsync(Guid id) => throw new NotImplementedException();

        Task<IEnumerable<User>> IReadRepository<User, Guid>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
