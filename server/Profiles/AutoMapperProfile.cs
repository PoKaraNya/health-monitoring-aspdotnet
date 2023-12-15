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
        CreateMap<Person, PersonDto>();
        CreateMap<CreatePersonRequestDto, Person>();
        CreateMap<UpdatePersonRequestDto, Person>();


        CreateMap<PersonRecord, PersonRecordDto>()
          .ForMember(_ => _.Room, opt => opt.MapFrom(src => src.Room))
          .ForMember(_ => _.Person, opt => opt.MapFrom(src => src.Person));

        CreateMap<CreatePersonRecordRequestDto, PersonRecord>()
           .ForMember(_ => _.RecordedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreatePersonRecordByDeviceRequestDto, PersonRecord>()
            .ForMember(_ => _.RecordedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreatePersonRecordByDeviceRequestDto, Person>();
        CreateMap<CreatePersonRecordByDeviceRequestDto, Room>();

        //CreateMap<UpdatePersonRecordRequestDto, PersonRecord>();


        CreateMap<Room, RoomDto>();
        CreateMap<CreateRoomRequestDto, Room>();
        CreateMap<UpdateRoomRequestDto, Room>();


        CreateMap<RoomRecord, RoomRecordDto>()
          .ForMember(_ => _.Room, opt => opt.MapFrom(src => src.Room));

        CreateMap<CreateRoomRecordRequestDto, RoomRecord>()
           .ForMember(_ => _.RecordedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreateRoomRecordByDeviceRequestDto, RoomRecord>()
          .ForMember(_ => _.RecordedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

        CreateMap<CreateRoomRecordByDeviceRequestDto, Room>();
        

        //CreateMap<UpdateRoomRecordRequestDto, RoomRecord>();


        //CreateMap<PersonRecord, PersonRecordDto>(); // ?
    }
}
