using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Locations.Commands;
using NZ.HRM.Application.Locations.Handlers;
using NZ.HRM.Application.Locations.Queries;

namespace NZ.HRM.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly LocationCommandHandler _commandHandler;
        private readonly LocationQueryHandler _queryHandler;

        public LocationsController(LocationCommandHandler commandHandler, LocationQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var location = await _queryHandler.Handle(new GetLocationByIdQuery(id));
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var locations = await _queryHandler.Handle(new GetAllLocationsQuery());
            return Ok(locations);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLocationCommand cmd)
        {
            var id = await _commandHandler.Handle(cmd);
            return Ok(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateLocationCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _commandHandler.Handle(cmd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _commandHandler.Handle(new DeleteLocationCommand(id));
            return NoContent();
        }
    }
}