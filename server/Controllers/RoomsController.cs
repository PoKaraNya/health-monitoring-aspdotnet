using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.DTO.Room;
using server.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RoomsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
  

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _unitOfWork.Room.GetAllAsync();
        //var rooms = await _unitOfWork.Room.GetAll();
        var obj = _mapper.Map<List<RoomDto>>(rooms);
        //var totalCount = await _unitOfWork.Persons.GetCountAsync();
        var response = new
        {
            data = obj,
            //maxPage = Math.Ceiling((double)totalCount / Constants.MaxItemsPerPage)
        };
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetRoomById([FromRoute] int? id, [FromQuery] string? roomNumber)
    {
        var existingObject = await _unitOfWork.Room.GetFirstOrDefault(x => x.RoomId == id);

        if (existingObject is null)
        {
            return NotFound();
        }

        var obj = _mapper.Map<RoomDto>(existingObject);

        var response = new
        {
            data = obj,
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
        await _unitOfWork.SaveAsync();

        var obj = _mapper.Map<RoomDto>(room);

        var response = new
        {
            data = obj,
        };
        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateRoom([FromRoute] int id, UpdateRoomRequestDto request)
    {
        var room = _mapper.Map<Room>(request);
        //var room = new Room
        //{
        //    RoomId = id,
        //    RoomNumber = request.RoomNumber,
        //    RoomType = request.RoomType,
        //};

        room = await _unitOfWork.Room.UpdateAsync(room, x => x.RoomId == id);

        if (room is null)
        {
            return NotFound();
        }

        var obj = _mapper.Map<RoomDto>(room);
        //var response = new RoomDto
        //{
        //    RoomId = room.RoomId,
        //    RoomNumber = room.RoomNumber,
        //    RoomType = room.RoomType,
        //};

        var response = new
        {
            data = obj,
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

        var obj = _mapper.Map<RoomDto>(room);
        //var obj = new RoomDto
        //{
        //    RoomId = room.RoomId,
        //    RoomNumber = room.RoomNumber,
        //    RoomType = room.RoomType,
        //};

        var response = new
        {
            data = obj,
        };
        return Ok(response);
    }
}
