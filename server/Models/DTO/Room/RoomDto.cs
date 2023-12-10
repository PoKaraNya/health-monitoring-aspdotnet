using server.Models.DTO.PersonRecord;
using server.Models.DTO.RoomRecord;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Room;

public class RoomDto
{
    public int RoomId { get; set; }
    public string RoomNumber { get; set; }
    public string RoomType { get; set; }
    //public ICollection<RoomRecordDto> RoomRecords { get; set; }
    //public ICollection<PersonRecordDto> PersonRecords { get; set; }
}
