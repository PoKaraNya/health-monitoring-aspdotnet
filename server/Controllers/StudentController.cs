using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.DTO.Person;
using server.Models.DTO.PersonRecord;
using server.Repository.IRepository;
using server.Utils;
using System.Security.Claims;

namespace server.Controllers;

public class StudentController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    //[Authorize]
    //[HttpPost("auth")]
    //public void Post(object value)
    //{
    //    ClaimsIdentity userIdentity = User.Identity as ClaimsIdentity;
    //    string userEmail = value.ToString();
    //}

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyData()
    {
        var firebaseUser = HttpContext.User;
        var userEmail = firebaseUser.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        var user = await _unitOfWork.Person.GetFirstOrDefault(x => x.Email == userEmail);
        var obj = _mapper.Map<List<PersonDto>>(user);
        
        var response = new
        {
            data = obj,
        };
        return Ok(response);
    }

    [Authorize]
    [HttpGet("records")]
    public async Task<IActionResult> GetMyData([FromQuery] int pageNumber = 1, bool isOutputOnlyCritical = false)
    {
        var firebaseUser = HttpContext.User;
        var userEmail = firebaseUser.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        var user = await _unitOfWork.Person.GetFirstOrDefault(x => x.Email == userEmail);
        if(user is null)
        {
            return NotFound();
        }
        var personId = user.PersonId;
        var personRecords = await _unitOfWork.PersonRecord.GetAllAsync(pageNumber, isOutputOnlyCritical, personId);

        var obj = _mapper.Map<List<PersonRecordDto>>(personRecords);

        var totalCount = await _unitOfWork.PersonRecord.GetCountAsync(isOutputOnlyCritical, personId);
        var response = new
        {
            data = obj,
            maxPage = Math.Ceiling((double)totalCount / Constants.MaxItemsPerPage)
        };
        return Ok(response);
    }

}
