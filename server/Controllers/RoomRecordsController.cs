using Microsoft.AspNetCore.Mvc;
using server.Repository.IRepository;
using server.Models.DTO.RoomRecord;
using server.Models;
using AutoMapper;
using System.Text.Json;
using server.Utils;
using System.Text.Json.Serialization;
using server.Models.DTO.PersonRecord;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomRecordsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomRecord>>> GetAllRoomRecords([FromQuery] int pageNumber = 1, bool isOutputOnlyCritical = false)
    {
        var roomRecords = await _unitOfWork.RoomRecord.GetAllWithRelationsAsync(pageNumber, isOutputOnlyCritical);

        if (roomRecords is null)
        {
            return NotFound();
        }

        var obj = _mapper.Map<List<RoomRecordDto>>(roomRecords);
        var totalCount = await _unitOfWork.RoomRecord.GetCountAsync(isOutputOnlyCritical);
        var response = new
        {
            data = obj,
            maxPage = Math.Ceiling((double)totalCount / Constants.MaxItemsPerPage)
        };

        return Ok(response);
        //return new JsonResult(response, options);
    }



    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<RoomRecord>>> GetRoomRecordByRoomId([FromRoute] int? id, [FromQuery] int pageNumber = 1, bool isOutputOnlyCritical = false)
    {
        var roomRecords = await _unitOfWork.RoomRecord.GetAllWithRelationsAsync(pageNumber, isOutputOnlyCritical, id);

        if (roomRecords is null)
        {
            return NotFound();
        }

        var obj = _mapper.Map<List<RoomRecordDto>>(roomRecords);

        var totalCount = await _unitOfWork.RoomRecord.GetCountAsync(isOutputOnlyCritical);
        var response = new
        {
            data = obj,
            maxPage = Math.Ceiling((double)totalCount / Constants.MaxItemsPerPage)
        };

        return Ok(response);
    }

    [HttpGet("Dashboard")]
    public async Task<ActionResult<IEnumerable<RoomRecord>>> GetAllRoomRecordDashboard([FromQuery] int? day, int? month, int year, int? id)
    {
        var roomRecords = await _unitOfWork.RoomRecord.GetAllRoomRecordDashboard(day, month, year, id);

        if (roomRecords is null)
        {
            return NotFound();
        }

        var obj = _mapper.Map<List<RoomRecordDto>>(roomRecords);

        var response = new
        {
            data = obj,
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoomRecord([FromBody] CreateRoomRecordRequestDto request)
    {
        var existingObject = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomId == request.RoomId);

        if (existingObject is null)
        {
            return NotFound();
        }

        var roomRecord = _mapper.Map<RoomRecord>(request);
       
        await _unitOfWork.RoomRecord.Add(roomRecord);
        await _unitOfWork.SaveAsync();

        var obj = _mapper.Map<RoomRecordDto>(roomRecord);

        var response = new
        {
            data = obj,
        };
        return Ok(response);
    }
}