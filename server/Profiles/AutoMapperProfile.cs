using AutoMapper;
using server.Models.DTO.Person;
using server.Models;
using server.Models.DTO.RoomRecord;
using server.Models.DTO.PersonRecord;

namespace server.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreatePersonRequestDto, Person>();
        CreateMap<UpdatePersonRequestDto, Person>();
        CreateMap<Person, PersonDto>();

        CreateMap<RoomRecord, RoomRecordDto>();
        CreateMap<CreateRoomRecordRequestDto, RoomRecord>();
        //CreateMap<UpdateRoomRecordRequestDto, RoomRecord>();

        CreateMap<PersonRecord, PersonRecordDto>();
        CreateMap<CreatePersonRecordRequestDto, PersonRecord>();
        //CreateMap<UpdatePersonRecordRequestDto, PersonRecord>();
    }
}
