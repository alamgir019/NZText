using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Locations.Commands;
using NZ.HRM.Application.SubUnits.Commands;
using NZ.HRM.Application.SubUnits.Handlers;
using NZ.HRM.Application.SubUnits.Queries;

namespace NZ.HRM.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubUnitsController : ControllerBase
    {
        private readonly SubUnitsCommandHandler _commandHandler;
        private readonly SubUnitsQueryHandler _queryHandler;

        public SubUnitsController(SubUnitsCommandHandler commandHandler, SubUnitsQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var subUnit = await _queryHandler.Handle(new GetSubUnitByIdQuery(id));
            if (subUnit == null) return NotFound();
            return Ok(subUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subUnits = await _queryHandler.Handle(new GetAllSubUnitsQuery());
            return Ok(subUnits);
        }

        [HttpGet("unit/{unitId}")]
        public async Task<IActionResult> GetByUnit(string unitId)
        {
            var subUnits = await _queryHandler.Handle(new GetSubUnitsByUnitIdQuery(unitId));
            return Ok(subUnits);
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(string employeeId)
        {
            var subUnits = await _queryHandler.Handle(new GetSubUnitsByEmployeeIdQuery(employeeId));
            return Ok(subUnits);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubUnitCommand cmd)
        {
            var id = await _commandHandler.Handle(cmd);
            return Ok(new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSubUnitCommand cmd)
        {
            if (id != cmd.Id) return BadRequest();
            await _commandHandler.Handle(cmd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _commandHandler.Handle(new DeleteSubUnitCommand(id));
            return NoContent();
        }
    }
}