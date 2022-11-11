using AutoMapper;
using KaupunkipyoraAPI.Context;

namespace KaupunkipyoraAPI.Contracts
{
    public interface IUnitOfWork
    {
        DapperContext _context { get; }
        IMapper _mapper { get; }

        IBikeRouteRepository BikeRouteRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
