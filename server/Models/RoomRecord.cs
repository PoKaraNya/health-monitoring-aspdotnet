using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models;

public class RoomRecord
{
    [Key]
    [Display(Name = "Room record ID")]
    public int RoomRecordId { get; set; }
    [ForeignKey("Room")]
    [Display(Name = "Room ID")]
    public int RoomId { get; set; }
    public virtual Room Room { get; set; }

    [Required]
    public double Humidity { get; set; }
    [Required]
    public double Temperature { get; set; }
    [Required]
    public double Pressure { get; set; }
    [Required]
    [Display(Name = "Carbon dioxide")]
    public double CarbonDioxide { get; set; }
    [Required]
    [Display(Name = "Air ions")]
    public double AirIons { get; set; }
    [Required]
    public double Ozone { get; set; }
    [Display(Name = "Is critical results")]
    public bool IsCriticalResults { get; set; }
    [Display(Name = "Recorded date")]
    public DateTime RecordedDate { get; set; }
}
