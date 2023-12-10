using server.Models.DTO.PersonRecord;
using server.Models.DTO.RoomRecord;
using server.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace server.Models.DTO.Person;

public class PersonDto
{
    public int PersonId { get; set; }

    public int? StudentID { get; set; }
    public string Name { get; set; }
    public string? StudyGroup { get; set; }
    [EnumDataType(typeof(Person_RoleAttribute.Role))]
    public string Role { get; set; }
    public string Email { get; set; }

    //public ICollection<PersonRecordDto> PersonRecords { get; set; }
}
