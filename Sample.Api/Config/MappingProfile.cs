using AutoMapper;
using Sample.Api.DTO;
using Sample.Api.Models;

namespace Sample.Api.Config;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FruitRequest, Fruit>();
    }
}
