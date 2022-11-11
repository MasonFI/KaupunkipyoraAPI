using KaupunkipyoraAPI.Models.Entity;

namespace KaupunkipyoraAPI.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByUsername(string username);
    }
}
