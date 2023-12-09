using AutoMapper;
using server.Models;
using server.Models.DTO.Person;
using server.Models.DTO.Room;
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

        CreateMap<CreateRoomRequestDto, Room>();
        CreateMap<UpdateRoomRequestDto, Room>();
        CreateMap<Room, RoomDto>();

        //CreateMap<RoomRecord, RoomRecordDto>()
        //    .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room));

        CreateMap<RoomRecord, RoomRecordDto> ()
           .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room));
        CreateMap<PersonRecord, PersonRecordDto>()
          .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
          .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));

        CreateMap<CreateRoomRecordRequestDto, RoomRecord>()
             .ForMember(dest => dest.RecordedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        //CreateMap<UpdateRoomRecordRequestDto, RoomRecord>();

        CreateMap<PersonRecord, PersonRecordDto>();
        CreateMap<CreatePersonRecordRequestDto, PersonRecord>()
             .ForMember(dest => dest.RecordedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
        //CreateMap<UpdatePersonRecordRequestDto, PersonRecord>();
    }
}
