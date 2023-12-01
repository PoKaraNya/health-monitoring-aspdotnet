using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.DTO.Person;
using server.Repository.IRepository;
using AutoMapper;


namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase 
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _unitOfWork.Person.GetAllAsync();
        var response = _mapper.Map<List<PersonDto>>(persons);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPersonById([FromRoute]int id)
    {
        var existingObject = await _unitOfWork.Person.GetFirstOrDefault(x => x.PersonId == id);
        if (existingObject is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<PersonDto>(existingObject);
     
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody]CreatePersonRequestDto request)
    {
        var person = _mapper.Map<Person>(request);
        await _unitOfWork.Person.Add(person);
        await _unitOfWork.SaveAsync();
        var response = _mapper.Map<PersonDto>(person);

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePerson([FromRoute]int id, UpdatePersonRequestDto request)
    {
        var existingPerson = await _unitOfWork.Person.GetFirstOrDefault(x => x.PersonId == id);
        if (existingPerson is null)
        {
            return NotFound();
        }
        _mapper.Map(request, existingPerson);
        await _unitOfWork.Person.UpdateAsync(existingPerson, x => x.PersonId == id);
        await _unitOfWork.SaveAsync();
        var response = _mapper.Map<PersonDto>(existingPerson);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
        var person = await _unitOfWork.Person.DeleteAsync(x => x.PersonId == id);
        if(person is null)
        { 
            return NotFound(); 
        }
        var response = _mapper.Map<PersonDto>(person);

        return Ok(response);
    }
}
