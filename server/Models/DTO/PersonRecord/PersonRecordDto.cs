using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.Validations;
using server.Models.DTO.Room;
using server.Models.DTO.Person;

namespace server.Models.DTO.PersonRecord
{
    public class PersonRecordDto
    {
       
        public int PersonRecordId { get; set; }

        public int PersonId { get; set; }
        public virtual PersonDto Person { get; set; }

        public int RoomId { get; set; }
        public virtual RoomDto Room { get; set; }

        public double Saturation { get; set; }
        public double HeartRate { get; set; }
        public double Temperature { get; set; }
        public bool IsCriticalResults { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}
