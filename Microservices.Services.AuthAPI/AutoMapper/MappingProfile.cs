using AutoMapper;
using Microservices.Services.AuthAPI.Models;
using Microservices.Services.AuthAPI.Models.Dtos.Register;
using Microservices.Services.AuthAPI.Models.Dtos.User;

namespace Microservices.Services.AuthAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserExtended, UserDto>().ReverseMap();
            CreateMap<UserExtended, RegisterRequestDto>().ReverseMap();
        }
    }
}
