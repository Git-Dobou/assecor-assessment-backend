using System.Collections.Generic;
using System.Linq;
using System.Net;
using assecor_assessment_backend.Models;
using assecor_assessment_backend.Services.Db;
using assecor_assessment_backend.Services.Logger;
using assecor_assessment_backend.Services.Person;
using Microsoft.AspNetCore.Mvc;

namespace assecor_assessment_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILoggerManager _logger;

        public PersonsController(IPersonService personService,
        ILoggerManager loggerManager)
        {
            _personService = personService;
            _logger = loggerManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            _logger.LogInfo("Get all peraons");

            var persons = _personService.GetAllPersons();

            if (!persons.Any())
                return StatusCode((int)HttpStatusCode.NoContent);

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetById(string id)
        {
            _logger.LogInfo($"Get person with id {id}");

            if (!int.TryParse(id, out int workId))
                return StatusCode((int)HttpStatusCode.BadRequest);

            var person = _personService.GetPersonById(workId);
            if (person == null)
                return StatusCode((int)HttpStatusCode.NoContent);

            return Ok(person);
        }

        [Route("color/{color}")]
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAllWithSameColor(string color)
        {
            _logger.LogInfo($"Get all persons with color {color}");

            var persons = _personService.GetAllWithSameColor(color);
            if (!persons.Any())
                return StatusCode((int)HttpStatusCode.NoContent);

            return Ok(persons);
        }

        [HttpPost]
        public ActionResult AddPerson([FromBody] Person person)
        {
            _logger.LogInfo($"Add new person");

            if (!person.IsComplet())
                return StatusCode((int)HttpStatusCode.PartialContent);

            if (!_personService.AddPerson(person))
                return StatusCode((int)HttpStatusCode.InternalServerError);

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
