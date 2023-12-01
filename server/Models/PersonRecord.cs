using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class PersonRecord
{
    [Key]
    [Display(Name = "Person record ID")]
    public int PersonRecordId { get; set; }
  
    [ForeignKey("Person")]
    [Display(Name = "Person ID")]
    public int PersonId { get; set; }
    public virtual Person Person { get; set; }

    [ForeignKey("Room")]
    [Display(Name = "Room ID")]
    public int RoomId { get; set; }
    public virtual Room Room { get; set; }

    [Required]
    public double Saturation { get; set; }
    [Required]
    public double HeartRate { get; set; }
    [Required]
    public double Temperature { get; set; }

    [Display(Name = "Is critical results")]
    public bool IsCriticalResults { get; set; }

    [Display(Name = "Recorded date")]
    public DateTime RecordedDate { get; set; }
}
