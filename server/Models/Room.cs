using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Room
{
    [Key]
    [Display(Name = "Room ID")]
    public int RoomId { get; set; }
  
    [Required]
    [Display(Name = "Room number")]
    public string RoomNumber { get; set; }

    [Required]
    [Display(Name = "Room type")]
    public string RoomType { get; set; }

    public virtual ICollection<RoomRecord> RoomRecords { get; set; }
    public virtual ICollection<PersonRecord> PersonRecords { get; set; }
}
