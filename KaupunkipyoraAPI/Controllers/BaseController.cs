using AutoMapper;
using KaupunkipyoraAPI.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace KaupunkipyoraAPI.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _UOW;
        protected readonly IMapper _mapper;

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UOW = unitOfWork;
            _mapper = mapper;
        }
    }
}
