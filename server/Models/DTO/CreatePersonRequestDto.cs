using server.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO;
public class CreatePersonRequestDto
{
    public int? StudentID { get; set; }

    [Required]
    public string Name { get; set; }

    public string? StudyGroup { get; set; }

    [Required]
    [EnumDataType(typeof(Person_RoleAttribute.Role))]
    public string Role { get; set; }

    [Required]
    public string Email { get; set; }
}
