using AutoMapper;
using KaupunkipyoraAPI.Context;
using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Repository;

namespace KaupunkipyoraAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public DapperContext _context { get; private set; }
        public IMapper _mapper { get; private set; }

        public UnitOfWork(DapperContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private IBikeRouteRepository? _BikeRouteRepository { get; set; }
        public IBikeRouteRepository BikeRouteRepository => _BikeRouteRepository ??= new BikeRouteRepository(_context, _mapper);
        private IUserRepository? _UserRepository { get; set; }
        public IUserRepository UserRepository => _UserRepository ??= new UserRepository(_context, _mapper);

    }
}
