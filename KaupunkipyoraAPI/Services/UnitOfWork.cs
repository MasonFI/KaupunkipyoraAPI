using KaupunkipyoraAPI.Contracts;

namespace KaupunkipyoraAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IBikeRouteRepository bikeRouteRepository, IUserRepository userRepository)
        {
            BikeRouteRepository = bikeRouteRepository;
            UserRepository = userRepository;
        }

        public IBikeRouteRepository BikeRouteRepository { get; }
        public IUserRepository UserRepository { get; }
    }
}
