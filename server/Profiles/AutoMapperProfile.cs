using AutoMapper;
using server.Models.DTO.Person;
using server.Models;

namespace server.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreatePersonRequestDto, Person>();
        CreateMap<UpdatePersonRequestDto, Person>();
        CreateMap<Person, PersonDto>();

    }
}
