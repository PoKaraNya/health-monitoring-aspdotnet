 using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repository.IRepository;
using server.Models.DTO.PersonRecord;
using server.Utils;
using Microsoft.AspNetCore.Authorization;
using server.Authentication;
using server.Models.DTO.RoomRecord;
using server.Services;

namespace server.Controllers;
[Authorize]
[ApiController]
[Route("[controller]")]
public class PersonRecordsController(IUnitOfWork unitOfWork, IMapper mapper, IPersonRecordService personRecordService) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IPersonRecordService _personRecordService = personRecordService;

    //[Authorize(Roles = UserRoles.Admin)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonRecord>>> GetAllPersonRecords([FromQuery] int pageNumber = 1, bool isOutputOnlyCritical = false)
    {
        var personRecords = await _unitOfWork.PersonRecord.GetAllWithRelationsAsync(pageNumber, isOutputOnlyCritical);
        if (personRecords is null)
        {
            return NotFound();
        }

        var obj = _mapper.Map<List<PersonRecordDto>>(personRecords);

        var totalCount = await _unitOfWork.RoomRecord.GetCountAsync(isOutputOnlyCritical);
        var response = new
        {
            data = obj,
            maxPage = Math.Ceiling((double)totalCount / Constants.MaxItemsPerPage)
        };

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

        var obj = _mapper.Map<List<PersonRecordDto>>(personRecords);

        var totalCount = await _unitOfWork.RoomRecord.GetCountAsync(isOutputOnlyCritical);
        var response = new
        {
            data = obj,
            maxPage = Math.Ceiling((double)totalCount / Constants.MaxItemsPerPage)
        };

        return Ok(response);
    }

    [HttpGet("Dashboard")]
    public async Task<ActionResult<IEnumerable<PersonRecord>>> GetAllPersonRecordDashboard([FromQuery] int? day, int? month, int year, int? id)
    {
        var personRecords = await _unitOfWork.PersonRecord.GetAllPersonRecordDashboard(day, month, year, id);

        if (personRecords is null)
        {
            return NotFound();
        }
       
        var obj = _mapper.Map<List<PersonRecordDto>>(personRecords);

        var response = new
        {
            data = obj,
        };
        return Ok(response);
    }

    [HttpPost("device")]
    public async Task<IActionResult> CreatePersonRecordByDevice([FromBody] CreatePersonRecordByDeviceRequestDto request)
    {
        var existingRoom = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomNumber == request.RoomNumber);
        var existingPerson = await _unitOfWork.Person.GetFirstOrDefault(x => x.StudentID == request.StudentID);
        
        if(existingPerson is null)
        {
            var person = _mapper.Map<Person>(request);
            await _unitOfWork.Person.Add(person);
            await _unitOfWork.SaveAsync();
            existingPerson = await _unitOfWork.Person.GetFirstOrDefault(x => x.StudentID == request.StudentID);
        }

        if (existingRoom is null)
        {
            var room = _mapper.Map<Room>(request);
            await _unitOfWork.Room.Add(room);
            await _unitOfWork.SaveAsync();
            existingRoom = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomNumber == request.RoomNumber);
        }

        var personRecord = _mapper.Map<PersonRecord>(request);
        personRecord.RoomId = existingRoom.RoomId;
        personRecord.PersonId = existingPerson.PersonId;
        personRecord.IsCriticalResults = _personRecordService.IsCriticalResults(request);

        await _unitOfWork.PersonRecord.Add(personRecord);
        await _unitOfWork.SaveAsync();

        var obj = _mapper.Map<PersonRecordDto>(personRecord);

        var response = new
        {
            data = obj,
        };
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> CreatePersonRecord([FromBody] CreatePersonRecordRequestDto request)
    {
        var existingPerson = await _unitOfWork.Person.GetFirstOrDefault(x => x.PersonId == request.PersonId);
        var existingRoom = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomId == request.RoomId);

        if (existingPerson is null || existingRoom is null)
        {
            return NotFound();
        }

        var personRecord = _mapper.Map<PersonRecord>(request);
        personRecord.IsCriticalResults = _personRecordService.IsCriticalResults(request);

        await _unitOfWork.PersonRecord.Add(personRecord);
        await _unitOfWork.SaveAsync();

        var obj = _mapper.Map<PersonRecordDto>(personRecord);

        var response = new
        {
            data = obj,
        };

        return Ok(response);
    }

}
