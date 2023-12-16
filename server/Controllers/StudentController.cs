using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Models.DTO.Person;
using server.Models.DTO.PersonRecord;
using server.Repository.IRepository;
using server.Utils;

namespace server.Controllers;

public class StudentController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    [HttpGet("me")]
    public async Task<IActionResult> GetMyData([FromBody] CreatePersonRequestDto request)
    {
        var email = request.Email;
        var user = await _unitOfWork.Person.GetFirstOrDefault(x => x.Email == email);
        var obj = _mapper.Map<List<PersonDto>>(user);
        
        var response = new
        {
            data = obj,
        };
        return Ok(response);
    }

    [HttpGet("records")]
    public async Task<IActionResult> GetMyData([FromBody] CreatePersonRequestDto request, [FromQuery] int pageNumber = 1, bool isOutputOnlyCritical = false)
    {
        var email = request.Email;
        var user = await _unitOfWork.Person.GetFirstOrDefault(x => x.Email == email);
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
