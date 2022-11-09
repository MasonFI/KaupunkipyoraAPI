namespace KaupunkipyoraAPI.Contracts
{
    public interface IUnitOfWork
    {
        IBikeRouteRepository BikeRouteRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
