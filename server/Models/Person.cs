using server.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class Person
{
    [Key]
    [Display(Name = "Person ID")]
    public int PersonId { get; set; }

    [Display(Name = "Student ID")]
    public int? StudentID { get; set; }

    [Required]
    public string Name { get; set; }

    [Display(Name = "Study group")]
    public string? StudyGroup { get; set; }

    [Required]
    [EnumDataType(typeof(Person_RoleAttribute.Role))]
    public string Role { get; set; }

    [Required]
    public string Email { get; set; }

    public virtual ICollection<PersonRecord> PersonRecord { get; set; }
}
