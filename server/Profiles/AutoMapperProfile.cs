using AutoMapper;
using server.Models.DTO.Person;
using server.Models;
using server.Models.DTO.RoomRecord;

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
    }
}
