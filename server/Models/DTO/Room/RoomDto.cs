using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Room;

public class RoomDto
{
    public int RoomId { get; set; }

    [Required]
    public string RoomNumber { get; set; }

    [Required]
    public string RoomType { get; set; }
}
