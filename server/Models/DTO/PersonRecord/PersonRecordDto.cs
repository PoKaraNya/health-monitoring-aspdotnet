using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using server.Models.Validations;

namespace server.Models.DTO.PersonRecord
{
    public class PersonRecordDto
    {
       
        public int PersonRecordId { get; set; }

        [Required]
        public double Saturation { get; set; }
        [Required]
        public double HeartRate { get; set; }
        [Required]
        public double Temperature { get; set; }
        public bool IsCriticalResults { get; set; }
        public DateTime RecordedDate { get; set; }

        public int PersonId { get; set; }
        public int? StudentID { get; set; }
        [Required]
        public string Name { get; set; }
        public string? StudyGroup { get; set; }
        [Required]
        [EnumDataType(typeof(Person_RoleAttribute.Role))]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }

        public int RoomId { get; set; }
        [Required]
        [Display(Name = "Room number")]
        public string RoomNumber { get; set; }
        [Required]
        [Display(Name = "Room type")]
        public string RoomType { get; set; }
    }
}
