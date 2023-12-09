using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.DTO.Room;

namespace server.Models.DTO.RoomRecord
{
    public class RoomRecordDto
    {
        public int RoomRecordId { get; set; }
        public int RoomId { get; set; }
        public virtual RoomDto Room { get; set; }
      
        public double Humidity { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double CarbonDioxide { get; set; }
        public double AirIons { get; set; }
        public double Ozone { get; set; }
        public bool IsCriticalResults { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}
