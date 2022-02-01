using System.Collections.Generic;
using System.Linq;
using System.Net;
using web_api.Models;
using web_api.Services.Logger;
using web_api.Services.Zoo;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZooController : ControllerBase
    {
        private readonly IZooService _zooService;
        private readonly ILoggerManager _logger;

        public ZooController(IZooService zooService,
        ILoggerManager loggerManager)
        {
            _zooService = zooService;
            _logger = loggerManager;
        }

        [HttpGet]
        public ContentResult GetInfo()
        {
            return base.Content(@"
<h1>Welcome to the Web Api</h1>
<ul> 
    <li>
        <span>GET: http://localhost:5000/zoo/all/</span>
    </li>
    <li>
        <span>GET: http://localhost:5000/zoo/get?name={name}/</span>
    </li>
    <li>
        <span>POST: http://localhost:5000/zoo/add/</span>
    </li>
    <li>
        <span>DELETE: http://localhost:5000/zoo/delete/</span>
    </li>
</ul>",
"text/html");
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<IEnumerable<Zoo>> GetAll()
        {
            _logger.LogInfo("Get all zoos");

            var zoos = _zooService.GetAll();

            return Ok(zoos);
        }

        [HttpGet()]
        [Route("get")]
        public ActionResult<IEnumerable<Zoo>> GetByName(string name)
        {
            _logger.LogInfo($"Get zoo with name {name}");
            if (string.IsNullOrEmpty(name?.Trim()))
                return StatusCode((int)HttpStatusCode.BadRequest);

            var zoo = _zooService.GetByName(name);
            if (zoo == null)
                return StatusCode((int)HttpStatusCode.NoContent);

            return Ok(zoo);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] Zoo zoo)
        {
            _logger.LogInfo($"Add new zoo");

            if (!zoo.IsComplet())
                return StatusCode((int)HttpStatusCode.PartialContent);

            if (!_zooService.Add(zoo))
                return StatusCode((int)HttpStatusCode.BadRequest);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete]
        [Route("delete")]
        public ActionResult Delete(string id)
        {
            _logger.LogInfo($"Delete zoo with the id : {id}");

            if (!_zooService.Delete(id))
                return StatusCode((int)HttpStatusCode.NotModified);

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
