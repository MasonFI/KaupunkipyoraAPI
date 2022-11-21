using AutoMapper;
using Dapper;
using KaupunkipyoraAPI.Context;
using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Models.DTO;
using KaupunkipyoraAPI.Models.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace KaupunkipyoraAPI.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected override string Table => "APIUser";
        protected override List<string> Columns => new()
        {
            PrimaryKey,
            "Username",
            "Email",
            "Created",
            "CreatedById",
            "Updated",
            "UpdatedById"
        };

        public UserRepository(DapperContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<User?> GetByUsername(string username)
        {
            var query = @$"SELECT {String.Join(",", Columns)}, Password FROM {Table} WHERE Username = @username";

            using var connection = _context.CreateConnection();
            var entity = await connection.QuerySingleOrDefaultAsync<User?>(query, new { username });
            return entity;
        }

        public override async Task<User> AddAsync(User entity) => throw new NotImplementedException();

        public override async Task<User> UpdateAsync(User entity) => throw new NotImplementedException();

        public override async Task<int> DeleteAsync(int id) => throw new NotImplementedException();
    }
}
