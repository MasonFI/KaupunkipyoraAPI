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
    public abstract class BaseRepository<T> : IGenericRepository<T>
        where T : class, IEntity
    {
        protected readonly DapperContext _context;
        protected readonly IMapper _mapper;

        protected abstract string Table { get; }
        protected abstract List<string> Columns { get; }

        protected virtual string PrimaryKey => "Id";

        public BaseRepository(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var query = @$"SELECT {String.Join(",", Columns)} FROM {Table} 
                WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var entity = await connection.QuerySingleOrDefaultAsync<T?>(query, new { id });
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT {String.Join(",", Columns)} FROM {Table}";
            using var connection = _context.CreateConnection();
            var entities = await connection.QueryAsync<T>(query);
            return entities.ToList();
        }

        public abstract Task<T> AddAsync(T entity);

        public abstract Task<T> UpdateAsync(T entity);

        public virtual async Task<int> DeleteAsync(int id)
        {
            var query = @$"DELETE FROM {Table} WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteAsync(query, new { id });
        }
    }
}
