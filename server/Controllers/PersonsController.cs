using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.DTO;
using server.Repository.IRepository;
using System;
using static server.Models.Validations.Person_RoleAttribute;
using System.Xml.Linq;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController(IUnitOfWork unitOfWork) : ControllerBase 
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        //List<Person> objCategoryList = _unitOfWork.Person.GetAll().ToList();
        
       var persons = await _unitOfWork.Person.GetAllAsync();
       var response = new List<PersonDto>();
       
        foreach (var person in persons)
        {
            response.Add(new PersonDto
            {
                PersonId = person.PersonId,
                StudentID = person.StudentID,
                Name = person.Name,
                StudyGroup = person.StudyGroup,
                Role = person.Role,
                Email = person.Email,
            });
        }

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPersonById([FromRoute]int id)
    {
        //var person = _unitOfWork.Person.GetFirstOrDefault(x => x.PersonId == id);
        //if (person == null)
        //{
        //    return NotFound();
        //}
        //return Ok(person);
        var existingObject = await _unitOfWork.Person.GetByIdAsync(id);
        if (existingObject is null)
        {
            return NotFound();
        }

        var response = new PersonDto
        {
            PersonId = existingObject.PersonId,
            StudentID = existingObject.StudentID,
            Name = existingObject.Name,
            StudyGroup = existingObject.StudyGroup,
            Role = existingObject.Role,
            Email = existingObject.Email,
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody]CreatePersonRequestDto request)
    {
        var person = new Person
        {
            StudentID = request.StudentID,
            Name = request.Name,
            StudyGroup = request.StudyGroup,
            Role = request.Role,
            Email = request.Email,
        };
        await _unitOfWork.Person.Add(person);
        await _unitOfWork.SaveAsync();

        var response = new PersonDto
        {
            PersonId = person.PersonId,
            StudentID = person.StudentID,
            Name = person.Name,
            StudyGroup = person.StudyGroup,
            Role = person.Role,
            Email = person.Email,
        };

        return Ok(response);

        //if (!ModelState.IsValid) return NotFound();//

        //_unitOfWork.Person.Add(person);
        //_unitOfWork.Save();
        //return Ok(person);
        //return "sdgjkl";
    }

    [HttpPut("{id}")]
    public string UpdatePerson(int? id)
    {
        //if (!ModelState.IsValid) return NotFound();

        ////if id = 0, asp by default add a new record
        //_unitOfWork.Person.Update(obj);
        //_unitOfWork.Save();

        //return Ok(obj);
        return "sdgjkl";
    }

    [HttpDelete("{id}")]
    public string DeletePerson(int id)
    {
        return "DeletePerson pe rson";
    }

}
