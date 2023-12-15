using server.Services;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.PersonRecord
{
    public class CreatePersonRecordByDeviceRequestDto : IPersonRecordRequest
    {
        public int PersonRecordId { get; set; }

        public int? StudentID { get; set; }
        public string Name { get; set; }
        public string? StudyGroup { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public string RoomNumber { get; set; }
        public string RoomType { get; set; }

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
