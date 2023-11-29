using Microsoft.AspNetCore.Mvc;
using server.Models.DTO.Person;
using server.Repository.IRepository;
using server.Repository;
using server.Models.DTO.RoomRecord;
using server.Models.DTO.Room;
using server.Models;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomRecordsController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAllRoomRecords()
    {
        //List<Person> objCategoryList = _unitOfWork.Person.GetAll().ToList();

        var roomRecords = await _unitOfWork.RoomRecord.GetAllAsync();
        var response = new List<RoomRecordDto>();

        foreach (var roomRecord in roomRecords)
        {
            response.Add(new RoomRecordDto
            {
                RoomRecordId = roomRecord.RoomRecordId,
                RoomId = roomRecord.RoomId,
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

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoomRecord([FromBody] CreateRoomRecordRequestDto request)
    {
        var roomRecord = new RoomRecord
        {
            RoomId = request.RoomId,
            Humidity = request.Humidity,
            Temperature = request.Temperature,
            Pressure = request.Pressure,
            CarbonDioxide = request.CarbonDioxide,
            AirIons = request.AirIons,
            Ozone = request.Ozone,
            IsCriticalResults = request.IsCriticalResults,
            RecordedDate = DateTime.Now,
        };
        await _unitOfWork.RoomRecord.Add(roomRecord);
        await _unitOfWork.SaveAsync();

        var response = new RoomRecordDto
        {
            RoomRecordId = roomRecord.RoomRecordId,
            RoomId = roomRecord.RoomId,
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
