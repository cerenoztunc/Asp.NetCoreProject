using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.API.DTOs;
using Project.Core.Models;
using Project.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IService<Person> _service;
        private readonly IMapper _mapper;
        public PersonsController(IService<Person> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _service.GetAllAsync();
            return Ok(_mapper.Map<PersonDto>(persons));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _service.GetByIdAsync(id);
            return Ok(_mapper.Map<PersonDto>(person));
        }
        [HttpPost]
        public async Task<IActionResult> Save(PersonDto personDto)
        {
            var newPerson = await _service.AddAsync(_mapper.Map<Person>(personDto));
            return Created(string.Empty,_mapper.Map<PersonDto>(newPerson));
        }
        [HttpPut]
        public IActionResult Update(PersonDto personDto)
        {
            var updatedPerson = _service.Update(_mapper.Map<Person>(personDto));
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletePerson = await _service.GetByIdAsync(id);
            _service.Remove(deletePerson);
            return NoContent();
        }
        
    }
}
