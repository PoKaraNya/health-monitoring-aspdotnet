using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.DTO.Room;
using server.Repository.IRepository;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _unitOfWork.Room.GetAllAsync();
        var response = new List<RoomDto>();

        foreach (var room in rooms)
        {
            response.Add(new RoomDto
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType,
            });
        }

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoomById([FromRoute] int id)
    {      
        var existingObject = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomId == id);
        if (existingObject is null)
        {
            return NotFound();
        }

        var response = new RoomDto
        {
            RoomId = existingObject.RoomId,
            RoomNumber = existingObject.RoomNumber,
            RoomType = existingObject.RoomType,
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequestDto request)
    {
        var room = new Room
        {
            RoomNumber = request.RoomNumber,
            RoomType = request.RoomType,
        };
        await _unitOfWork.Room.Add(room);
        await _unitOfWork.Room.Add(room);
        await _unitOfWork.SaveAsync();

        var response = new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            RoomType = room.RoomType,
        };

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom([FromRoute] int id, UpdateRoomRequestDto request)
    {
        var room = new Room
        {
            RoomId = id,
            RoomNumber = request.RoomNumber,
            RoomType = request.RoomType,
        };

        room = await _unitOfWork.Room.UpdateAsync(room, x => x.RoomId == id);

        if (room is null)
        {
            return NotFound();
        }

        var response = new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            RoomType = room.RoomType,
        };

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteRoom([FromRoute] int id)
    {
        var room = await _unitOfWork.Room.DeleteAsync(x => x.RoomId == id);
        if (room is null)
        {
            return NotFound();
        }

        var response = new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            RoomType = room.RoomType,
        };

        return Ok(response);
    }
}
