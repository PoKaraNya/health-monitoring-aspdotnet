using server.Services;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.PersonRecord
{
    public class CreatePersonRecordByDeviceRequestDto : IPersonRecordRequest
    {
        public int PersonRecordId { get; set; }

        public int StudentID { get; set; }
        public string RoomNumber { get; set; }

        [Required]
        public double Saturation { get; set; }
        [Required]
        public double HeartRate { get; set; }
        [Required]
        public double Temperature { get; set; }
        //public bool IsCriticalResults { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}
