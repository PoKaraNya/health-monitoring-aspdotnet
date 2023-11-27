using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class PersonRecord
{
    [Key]
    [Display(Name = "Person record ID")]
    public int PersonRecordId { get; set; }
    //[Key]
    [ForeignKey("Person")]
    [Display(Name = "Person ID")]
    public int PersonId { get; set; }

    //[Key]
    [ForeignKey("Room")]
    [Display(Name = "Room ID")]
    public int RoomId { get; set; }

    [Required]
    public double Saturation { get; set; }
    [Required]
    public double HeartRate { get; set; }
    [Required]
    public double Temperature { get; set; }

    [Display(Name = "Is critical results")]
    public double IsCriticalResults { get; set; }

    [Display(Name = "Recorded date")]
    public DateTime RecordedDate { get; set; }
}
