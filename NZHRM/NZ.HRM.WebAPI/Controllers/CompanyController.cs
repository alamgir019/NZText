using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Companies.Commands.CreateCompany;
using NZ.HRM.Application.Companies.Commands.DeleteCompany;
using NZ.HRM.Application.Companies.Commands.UpdateCompany;
using NZ.HRM.Application.Companies.Handlers;
using NZ.HRM.Application.Companies.Queries.GetAllCompanies;
using NZ.HRM.Application.Companies.Queries.GetCompanyById;

namespace NZ.HRM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly CompaniesQueryHandler _companiesQueryHandler;
    private readonly CompaniesCommandHandler _companyCommandHandler;

    public CompanyController(
        CompaniesQueryHandler companiesQueryHandler,
        CompaniesCommandHandler companyCommandHandler)
    {
        _companiesQueryHandler = companiesQueryHandler;
        _companyCommandHandler = companyCommandHandler;
    }

    /// <summary>
    /// Get all companies
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeInactive = false)
    {
        var query = new GetAllCompaniesQuery { IncludeInactive = includeInactive };
        var companies = await _companiesQueryHandler.Handle(query);
        return Ok(companies);
    }

    /// <summary>
    /// Get company by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CompanyDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string id)
    {
        var query = new GetCompanyByIdQuery { Id = id };
        var company = await _companiesQueryHandler.Handle(query);

        if (company == null)
            return NotFound(new { message = $"Company with ID {id} not found" });

        return Ok(company);
    }

    /// <summary>
    /// Create a new company
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCompanyCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var companyId = await _companyCommandHandler.Handle(command);
        return CreatedAtAction(nameof(GetById), new { id = companyId }, new { id = companyId });
    }

    /// <summary>
    /// Update an existing company
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateCompanyCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "ID mismatch" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            await _companyCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Delete a company (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteCompanyCommand { Id = id };

        try
        {
            await _companyCommandHandler.Handle(command);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}