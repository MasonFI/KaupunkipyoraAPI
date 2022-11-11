using AutoMapper;
using KaupunkipyoraAPI.Contracts;
using KaupunkipyoraAPI.Services.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace KaupunkipyoraAPI.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _UOW;
        protected readonly IMapper _mapper;
        protected APIOptions _APIOptions;

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper, IOptionsMonitor<APIOptions> options)
        {
            _UOW = unitOfWork;
            _mapper = mapper;
            _APIOptions = options.CurrentValue;
        }
    }
}
