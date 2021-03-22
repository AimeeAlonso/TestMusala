using AutoMapper;
using Services.Gateway.Dtos;
using Domain;

namespace Services.Mappings
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Gateway, GatewayDto>();
            CreateMap<GatewayDto,Domain.Gateway>();
            CreateMap<GatewayDetailsDto, Domain.Gateway>().ReverseMap();
            CreateMap<DeviceDto, Device>().ReverseMap();
        }
    }
}
