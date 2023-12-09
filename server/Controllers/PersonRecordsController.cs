using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repository.IRepository;
using server.Models.DTO.PersonRecord;
using server.Utils;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonRecordsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };

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
            data = JsonSerializer.Serialize(obj, options),
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
            data = JsonSerializer.Serialize(obj, options),
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
            data = JsonSerializer.Serialize(obj, options),
        };
        return Ok(response);
    }


    [HttpPost]
    public async Task<IActionResult> CreatePersonRecord([FromBody] CreatePersonRecordRequestDto request)
    {
        var existingObject = await _unitOfWork.Person.GetFirstOrDefault(x => x.PersonId == request.PersonId);
        var existingObject2 = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomId == request.RoomId);

        if (existingObject is null || existingObject2 is null)
        {
            return NotFound();
        }

        var personRecord = _mapper.Map<PersonRecord>(request);

        await _unitOfWork.PersonRecord.Add(personRecord);
        await _unitOfWork.SaveAsync();

        var obj = _mapper.Map<PersonRecordDto>(personRecord);

        var response = new
        {
            data = JsonSerializer.Serialize(obj, options),
        };

        return Ok(response);
    }

}
