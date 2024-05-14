using AutoMapper;

namespace Microservices.Services.Product.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Product, Models.Dtos.ProductDto>().ReverseMap();
        }
    }
}
