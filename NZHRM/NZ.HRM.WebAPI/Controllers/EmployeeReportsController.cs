using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Employees.Handlers;
using NZ.HRM.Application.Model.EmployeeReports.DTOs;
using NZ.HRM.Application.Services;
using NZ.HRM.Utility;
using NZ.HRM.Utility.Enum;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeReportsController : ControllerBase
{
    private readonly EmployeeQueryHandler _employeeQueryHandler;
    private readonly IEmployeeExcelExportService _excelExportService;

    public EmployeeReportsController(
        EmployeeQueryHandler employeeQueryHandler,
        IEmployeeExcelExportService excelExportService)
    {
        _employeeQueryHandler = employeeQueryHandler;
        _excelExportService = excelExportService;
    }

    [HttpGet("master-list")]
    [ProducesResponseType(typeof(Application.Model.Employees.DTOs.EmployeeMasterListResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployeeMasterList(
        [FromQuery] string? employeeCode = null,
        [FromQuery] string? employeeMobile = null,
        [FromQuery] Religion? religion = null,
        [FromQuery] Gender? gender = null,
        [FromQuery] string? gradeId = null,
        [FromQuery] string? shiftId = null,
        [FromQuery] string? divisionId = null,
        [FromQuery] string? employeeNID = null,
        [FromQuery] string? unitId = null,
        [FromQuery] string? subUnitId = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] string? sectionId = null,
        [FromQuery] string? cellId = null,
        [FromQuery] string? employeeNature = null,
        [FromQuery] RegisterType? registerType = null,
        [FromQuery] DateOnly? joiningFromDate = null,
        [FromQuery] DateOnly? joiningToDate = null,
        [FromQuery] DateOnly? fromDate = null,
        [FromQuery] DateOnly? toDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeInactive = false)
    {
        var query = new Application.Employees.Queries.GetEmployeeMasterList.GetEmployeeMasterListQuery
        {
            EmployeeCode = employeeCode,
            EmployeeMobile = employeeMobile,
            Religion = religion,
            Gender = gender,
            GradeId = gradeId,
            ShiftId = shiftId,
            DivisionId = divisionId,
            IdNumber = employeeNID,
            UnitId = unitId,
            SubUnitId = subUnitId,
            DepartmentId = departmentId,
            SectionId = sectionId,
            CellId = cellId,
            EmployeeNature = employeeNature,
            RegisterType = registerType ?? RegisterType.Master,
            JoiningFromDate = joiningFromDate,
            JoiningToDate = joiningToDate,
            FromDate = fromDate,
            ToDate = toDate,
            PageNumber = pageNumber,
            PageSize = pageSize,
            IncludeInactive = includeInactive
        };

        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);
        return Ok(result);
    }

    [HttpGet("master-list/export")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExportEmployeeMasterListToExcel(
        [FromQuery] string? employeeCode = null,
        [FromQuery] string? employeeMobile = null,
        [FromQuery] Religion? religion = null,
        [FromQuery] Gender? gender = null,
        [FromQuery] string? gradeId = null,
        [FromQuery] string? shiftId = null,
        [FromQuery] string? divisionId = null,
        [FromQuery] string? employeeNID = null,
        [FromQuery] string? unitId = null,
        [FromQuery] string? subUnitId = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] string? sectionId = null,
        [FromQuery] string? cellId = null,
        [FromQuery] string? employeeNature = null,
        [FromQuery] DateOnly? joiningFromDate = null,
        [FromQuery] DateOnly? joiningToDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeInactive = false)
    {
        try
        {
            // Fetch all matching records without pagination for export
            var query = new Application.Employees.Queries.GetEmployeeMasterList.GetEmployeeMasterListQuery
            {
                EmployeeCode = employeeCode,
                EmployeeMobile = employeeMobile,
                Religion = religion,
                Gender = gender,
                GradeId = gradeId,
                ShiftId = shiftId,
                DivisionId = divisionId,
                IdNumber = employeeNID,
                UnitId = unitId,
                SubUnitId = subUnitId,
                DepartmentId = departmentId,
                SectionId = sectionId,
                CellId = cellId,
                EmployeeNature = employeeNature,
                JoiningFromDate = joiningFromDate,
                JoiningToDate = joiningToDate,
                PageNumber = 1,
                PageSize = 1000,
                IncludeInactive = includeInactive
            };
            var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

            if (result.Employees == null || result.Employees.Count == 0)
            {
                return BadRequest(new { message = "No records found for the specified filters" });
            }

            // Generate Excel file
            var excelFileContents = await _excelExportService.GenerateEmployeeMasterListExcelAsync(result.Employees);

            // Return Excel file for download
            var fileName = $"EmployeeMasterList_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xlsx";
            return File(excelFileContents, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                fileName);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Error generating Excel file", error = ex.Message });
        }
    }

    [HttpGet("it-activation-summary")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetItActivationSummary([FromQuery] string unitId = "")
    {
        var query = new Application.Employees.Queries.GetItActivationSummary.GetItActivationSummaryQuery { UnitId = unitId, status = "ITActivation", includeInactive = false, date = DateTime.UtcNow };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);
        return Ok(result);
    }

    [HttpGet("medical-report/{employeeId}")]
    [ProducesResponseType(typeof(MedicalReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMedicalReport(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            return BadRequest(new { message = "employeeId is required" });

        var query = new Application.Employees.Queries.GetMedicalReport.GetMedicalReportQuery { EmployeeId = employeeId };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

        if (result == null)
            return NotFound(new { message = $"Medical report not found for employee with ID {employeeId}" });

        return Ok(result);
    }

    [HttpGet("candidate-entry/{employeeId}")]
    [ProducesResponseType(typeof(CandidateEntryReportDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCandidateEntryReport(string employeeId)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            return BadRequest(new { message = "employeeId is required" });

        var query = new Application.Employees.Queries.GetCandidateEntryReport.GetCandidateEntryReportQuery { EmployeeId = employeeId };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

        if (result == null)
            return NotFound(new { message = $"Candidate entry information not found for employee with ID {employeeId}" });

        return Ok(result);
    }

    [HttpGet("employee-detailed-profile/{employeeCode}")]
    [ProducesResponseType(typeof(Application.Model.Employees.DTOs.EmployeeDetailedProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeDetailedProfile(string employeeCode)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
            return BadRequest(new { message = "employeeCode is required" });

        var query = new Application.Employees.Queries.GetEmployeeDetailedProfile.GetEmployeeDetailedProfileQuery 
        { 
            EmployeeCode = employeeCode 
        };
        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

        if (result == null)
            return NotFound(new { message = $"Detailed profile not found for employee with code {employeeCode}" });

        return Ok(result);
    }

    [HttpGet("joining-letter/{employeeId}")]
    [ProducesResponseType(typeof(JoiningLetterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetJoiningLetter([FromRoute] string employeeId)
    {
        var query = new Application.Employees.Queries.GetJoiningLetter.GetJoiningLetterQuery
        {
            EmployeeId = employeeId
        };

        var result = await _employeeQueryHandler.Handle(query, cancellationToken: default);

        if (result == null)
            return NotFound(new { message = $"Employee with ID {employeeId} not found" });

        return Ok(result);
    }

}
