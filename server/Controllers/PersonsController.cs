﻿using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.DTO.Person;
using server.Repository.IRepository;
using AutoMapper;
using System.Text.Json.Serialization;
using System.Text.Json;
using server.Utils;


namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase 
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };

    [HttpGet]
    public async Task<IActionResult> GetAllPersons([FromQuery] int pageNumber = 1)
    {
        var persons = await _unitOfWork.Person.GetAllAsync();
        var obj = _mapper.Map<List<PersonDto>>(persons);
        //var totalCount = await _unitOfWork.Persons.GetCountAsync();
        var response = new
        {
            data = JsonSerializer.Serialize(obj, options),
            //maxPage = Math.Ceiling((double)totalCount / Constants.MaxItemsPerPage)
        };
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

        var obj = _mapper.Map<PersonDto>(existingObject);
        var response = new
        {
            data = JsonSerializer.Serialize(obj, options)
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody]CreatePersonRequestDto request)
    {
        var person = _mapper.Map<Person>(request);
        await _unitOfWork.Person.Add(person);
        await _unitOfWork.SaveAsync();
        var obj = _mapper.Map<PersonDto>(person);

        var response = new
        {
            data = JsonSerializer.Serialize(obj, options),
        };
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
        var obj = _mapper.Map<PersonDto>(existingPerson);
        
        var response = new
        {
            data = JsonSerializer.Serialize(obj, options),
        };
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
        var obj = _mapper.Map<PersonDto>(person);
        
        var response = new
        {
            data = JsonSerializer.Serialize(obj, options),
        };
        return Ok(response);
    }
}
