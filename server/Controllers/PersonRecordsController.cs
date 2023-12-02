using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repository.IRepository;
using server.Models.DTO.PersonRecord;
using server.Models.DTO.Room;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonRecordsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonRecord>>> GetAllPersonRecords([FromQuery] int pageNumber = 1, bool isOutputOnlyCritical = false)
    {
        var personRecords = await _unitOfWork.PersonRecord.GetAllWithRelationsAsync(pageNumber, isOutputOnlyCritical);
        if (personRecords is null)
        {
            return NotFound();
        }

        var response = new List<PersonRecordDto>();
        foreach (var personRecord in personRecords)
        {
            response.Add(new PersonRecordDto
            {
                PersonRecordId = personRecord.PersonRecordId,

                RoomId = personRecord.Room.RoomId,
                RoomNumber = personRecord.Room.RoomNumber,
                RoomType = personRecord.Room.RoomType,

                PersonId = personRecord.Person.PersonId,
                StudentID = personRecord.Person.StudentID,
                Name = personRecord.Person.Name,
                StudyGroup = personRecord.Person.StudyGroup,
                Role = personRecord.Person.Role,
                Email = personRecord.Person.Email,

                Saturation = personRecord.Saturation,
                HeartRate = personRecord.HeartRate,
                Temperature = personRecord.Temperature,
                IsCriticalResults = personRecord.IsCriticalResults,
                RecordedDate = personRecord.RecordedDate,
            });
        }

        //var response = _mapper.Map<List<PersonRecordDto>>(personRecords);
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<PersonRecord>>> GetPersonRecordByPersonId([FromRoute] int? id, [FromQuery] int pageNumber = 1, bool isOutputOnlyCritical = false)
    {
        var personRecords = await _unitOfWork.PersonRecord.GetAllWithRelationsAsync(pageNumber, isOutputOnlyCritical, id);
        if (personRecords is null)
        {
            return NotFound();
        }

        var response = new List<PersonRecordDto>();
        foreach (var personRecord in personRecords)
        {
            response.Add(new PersonRecordDto
            {
                PersonRecordId = personRecord.PersonRecordId,

                RoomId = personRecord.Room.RoomId,
                RoomNumber = personRecord.Room.RoomNumber,
                RoomType = personRecord.Room.RoomType,

                PersonId = personRecord.Person.PersonId,
                StudentID = personRecord.Person.StudentID,
                Name = personRecord.Person.Name,
                StudyGroup = personRecord.Person.StudyGroup,
                Role = personRecord.Person.Role,
                Email = personRecord.Person.Email,

                Saturation = personRecord.Saturation,
                HeartRate = personRecord.HeartRate,
                Temperature = personRecord.Temperature,
                IsCriticalResults = personRecord.IsCriticalResults,
                RecordedDate = personRecord.RecordedDate,
            });
        }

        //var response = _mapper.Map<List<PersonRecordDto>>(personRecords);
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> CreatePersonRecord([FromBody] CreatePersonRecordRequestDto request)
    {
        var utcTime = DateTime.UtcNow;

        var personRecord = new PersonRecord
        {
            PersonId = request.PersonId,
            RoomId = request.RoomId,
            Saturation = request.Saturation,
            HeartRate = request.HeartRate,
            Temperature = request.Temperature,
            IsCriticalResults = request.IsCriticalResults,
            RecordedDate = utcTime,
        };
        await _unitOfWork.PersonRecord.Add(personRecord);
        await _unitOfWork.SaveAsync();

        var existingObject = await _unitOfWork.Person.GetFirstOrDefault(x => x.PersonId == request.PersonId);
        var existingObject2 = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomId == request.RoomId);

        if (existingObject is null)
        {
            return NotFound();
        }

        var response = new PersonRecordDto
        {
            PersonRecordId = personRecord.PersonRecordId,

            RoomId = existingObject2.RoomId,
            RoomNumber = existingObject2.RoomNumber,
            RoomType = existingObject2.RoomType,

            PersonId = existingObject.PersonId,
            StudentID = existingObject.StudentID,
            Name = existingObject.Name,
            StudyGroup = existingObject.StudyGroup,
            Role = existingObject.Role,
            Email = existingObject.Email,

            Saturation = personRecord.Saturation,
            HeartRate = personRecord.HeartRate,
            Temperature = personRecord.Temperature,
            IsCriticalResults = personRecord.IsCriticalResults,
            RecordedDate = personRecord.RecordedDate,
        };

        return Ok(response);
    }

}
