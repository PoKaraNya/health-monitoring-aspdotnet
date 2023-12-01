using Microsoft.AspNetCore.Mvc;
using server.Repository.IRepository;
using server.Models.DTO.RoomRecord;
using server.Models;
using AutoMapper;
using server.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;

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

        var response = _mapper.Map<List<RoomRecordDto>>(roomRecords);




        //foreach (var roomRecord in roomRecords)
        //{
        //    //var roomNumber = roomRecord.Room.RoomNumber;
        //    response.Add(new RoomRecordDto
        //    {
        //        RoomRecordId = roomRecord.RoomRecordId,

        //        RoomId = roomRecord.Room.RoomId,
        //        RoomNumber = roomRecord.Room.RoomNumber,
        //        RoomType = roomRecord.Room.RoomType,

        //        Humidity = roomRecord.Humidity,
        //        Temperature = roomRecord.Temperature,
        //        Pressure = roomRecord.Pressure,
        //        CarbonDioxide = roomRecord.CarbonDioxide,
        //        AirIons = roomRecord.AirIons,
        //        Ozone = roomRecord.Ozone,
        //        IsCriticalResults = roomRecord.IsCriticalResults,
        //        RecordedDate = roomRecord.RecordedDate,
        //    });
        //}

       
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
