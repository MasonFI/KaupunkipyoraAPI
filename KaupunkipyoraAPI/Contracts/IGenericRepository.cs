using KaupunkipyoraAPI.Models.DTO;
using KaupunkipyoraAPI.Contracts;
using System.Data.SqlClient;

namespace KaupunkipyoraAPI.Contracts
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
