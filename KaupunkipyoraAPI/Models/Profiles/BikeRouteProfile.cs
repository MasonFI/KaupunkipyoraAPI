using AutoMapper;
using KaupunkipyoraAPI.Models.DTO;
using KaupunkipyoraAPI.Models.Entity;

namespace KaupunkipyoraAPI.Models.Profiles
{
    public class BikeRouteProfile : Profile
    {
        public BikeRouteProfile()
        {
            CreateMap<BikeRoute, BikeRouteDTO>();
        }
    }
}
