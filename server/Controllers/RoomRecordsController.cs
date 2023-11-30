using Microsoft.AspNetCore.Mvc;
using server.Models.DTO.Person;
using server.Repository.IRepository;
using server.Repository;
using server.Models.DTO.RoomRecord;
using server.Models.DTO.Room;
using server.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System.Reflection;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomRecordsController(IUnitOfWork unitOfWork, ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;
   
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomRecord>>> GetAllRoomRecords()
    {
        //List<Person> objCategoryList = _unitOfWork.Person.GetAll().ToList();
        //var roomRecords = await _unitOfWork.RoomRecord.GetAllWithRelationsAsync();
        ////var roomRecords = await _context.RoomRecords.Include(r => r.Room).ToListAsync();
        
        var roomRecords = await _unitOfWork.RoomRecord.GetAllWithRelationsAsync();
        var response = new List<RoomRecordDto>();

        foreach (var roomRecord in roomRecords)
        {
            //var roomNumber = roomRecord.Room.RoomNumber;
            response.Add(new RoomRecordDto
            {
                RoomRecordId = roomRecord.RoomRecordId,

                RoomId = roomRecord.Room.RoomId,
                RoomNumber = roomRecord.Room.RoomNumber,
                RoomType = roomRecord.Room.RoomType,

                Humidity = roomRecord.Humidity,
                Temperature = roomRecord.Temperature,
                Pressure = roomRecord.Pressure,
                CarbonDioxide = roomRecord.CarbonDioxide,
                AirIons = roomRecord.AirIons,
                Ozone = roomRecord.Ozone,
                IsCriticalResults = roomRecord.IsCriticalResults,
                RecordedDate = roomRecord.RecordedDate,
            });
        }
        //var options = new JsonSerializerOptions
        //{
        //    ReferenceHandler = ReferenceHandler.Preserve
        //};
        //var json = JsonSerializer.Serialize(roomRecords, options);
        //return Ok(mapper.Map<List<RoomRecord>>(roomRecords));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoomRecord([FromBody] CreateRoomRecordRequestDto request)
    {
        DateTime localTime = DateTime.Now; // Получение локального времени
        DateTime utcTime = localTime.ToUniversalTime(); // Преобразование в UTC
        var roomRecord = new RoomRecord
        {
            RoomId = request.RoomId,
            //RoomId = roomRecord.Room.RoomId,
            //RoomNumber = roomRecord.Room.RoomNumber,
            //RoomType = roomRecord.Room.RoomType,
            Humidity = request.Humidity,
            Temperature = request.Temperature,
            Pressure = request.Pressure,
            CarbonDioxide = request.CarbonDioxide,
            AirIons = request.AirIons,
            Ozone = request.Ozone,
            IsCriticalResults = request.IsCriticalResults,
            RecordedDate = utcTime,
        };
        await _unitOfWork.RoomRecord.Add(roomRecord);
        await _unitOfWork.SaveAsync();

        var existingObject = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomId == request.RoomId);
        if (existingObject is null)
        {
            return NotFound();
        }

        var response = new RoomRecordDto
        {
            RoomRecordId = roomRecord.RoomRecordId,
            //RoomId = roomRecord.RoomId,
            RoomId = existingObject.RoomId,
            RoomNumber = existingObject.RoomNumber,
            RoomType = existingObject.RoomType,

            Humidity = roomRecord.Humidity,
            Temperature = roomRecord.Temperature,
            Pressure = roomRecord.Pressure,
            CarbonDioxide = roomRecord.CarbonDioxide,
            AirIons = roomRecord.AirIons,
            Ozone = roomRecord.Ozone,
            IsCriticalResults = roomRecord.IsCriticalResults,
            RecordedDate = roomRecord.RecordedDate,
        };

        return Ok(response);
    }
}
