using Dapper;
using HexaPSA.Tool.Application.Interfaces.Repositories;
using HexaPSA.Tool.Domain.Entities;
using HexaPSA.Tool.Infrastructure.DataContexts;
using System.Data;

namespace HexaPSA.Tool.Infrastructure.Repositories
{
    public class UserLoginRepository:IUserLoginRepository
    {
        private readonly DapperDataContext _context;

        public UserLoginRepository(DapperDataContext context)
        {
            _context = context;
        }

        public async Task<UserLogin> AddAsync(UserLogin userLogin)
        {
            var query = "INSERT INTO UserLogin (UserId,PasswordHash,PasswordSalt, CreatedDate, CreatedBy,IsVerified) OUTPUT inserted.Id VALUES (@UserId,@PasswordHash,@PasswordSalt, @CreatedDate, @CreatedBy,@IsVerified)";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", userLogin.UserId, DbType.Guid);
            parameters.Add("PasswordHash", userLogin.PasswordHash, DbType.String);
            parameters.Add("PasswordSalt", userLogin.PasswordSalt, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
            parameters.Add("CreatedBy", userLogin.CreatedBy, DbType.Guid);
            parameters.Add("IsVerified", false, DbType.Boolean);

            using (var connection = _context.CreateConnection())
            {
                var Id = await connection.QuerySingleAsync<Guid>(query, parameters);
                var createdUserLogin = new UserLogin
                {
                    Id = Id,
                    UserId = userLogin.UserId,
                    PasswordHash = userLogin.PasswordHash,
                    PasswordSalt = userLogin.PasswordSalt,
                };
                return createdUserLogin;
            }
        }

        public Task<UserLogin> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<UserLogin>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserLogin> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM [UserLogin] WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleOrDefaultAsync<UserLogin>(query, new { id });
                return item;
            }
        }

        public Task<UserLogin> GetByNameAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserLogin> UpdateAsync(UserLogin item)
        {
            throw new NotImplementedException();
        }
    }
}
