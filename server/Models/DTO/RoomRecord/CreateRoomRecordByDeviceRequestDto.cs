using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using server.Services;

namespace server.Models.DTO.RoomRecord;

public class CreateRoomRecordByDeviceRequestDto : IRoomRecordRequest
{
    [ForeignKey("Room")]
    public string RoomNumber { get; set; }
    public string RoomType { get; set; }
    
    public double Humidity { get; set; }
    public double Temperature { get; set; }
    public double Pressure { get; set; }
    public double CarbonDioxide { get; set; }
    public double AirIons { get; set; }
    public double Ozone { get; set; }
    //public bool IsCriticalResults { get; set; }
    //public DateTime RecordedDate { get; set; }
}
