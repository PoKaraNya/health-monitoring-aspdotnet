using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Models.DTO;
using server.Repository.IRepository;
using System;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonsController(IUnitOfWork unitOfWork) : ControllerBase 
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [HttpGet]
    public string GetPersons()
    {
        //List<Person> objCategoryList = _unitOfWork.Person.GetAll().ToList();
        //return objCategoryList;
        return "sdgjkl";
    }

    [HttpGet("{id}")]
    public string GetPersonById(int id)
    {
        //var person = _unitOfWork.Person.GetFirstOrDefault(x => x.PersonId == id);
        //if (person == null)
        //{
        //    return NotFound();
        //}
        //return Ok(person);
        return "sdgjkl";
    }

    [HttpPost]
    public string CreatePerson([FromBody] Person person)//CreatePersonRequestDto
    {
        //if (!ModelState.IsValid) return NotFound();//

        //_unitOfWork.Person.Add(person);
        //_unitOfWork.Save();
        //return Ok(person);
        return "sdgjkl";
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
