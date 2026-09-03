using Microsoft.AspNetCore.Mvc;
using NZ.HRM.Application.Common;
using NZ.HRM.Application.LearnerAdjustments.Commands;
using NZ.HRM.Application.LearnerAdjustments.Handlers;
using NZ.HRM.Application.LearnerAdjustments.Queries;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;
using NZ.HRM.Application.Services;

namespace NZ.HRM.WebAPI.Controllers;

[ApiController]
[Route("api/learners/eligible-adjustments")]
public class LearnerAdjustmentsController : ControllerBase
{
    private readonly EligibleLearnerQueryHandler _eligibleLearnerQueryHandler;
    private readonly LearnerConfirmationCommandHandler _learnerConfirmationCommandHandler;
    private readonly IEligibleLearnerExcelExportService _excelExportService;

    public LearnerAdjustmentsController(
        EligibleLearnerQueryHandler eligibleLearnerQueryHandler,
        LearnerConfirmationCommandHandler learnerConfirmationCommandHandler,
        IEligibleLearnerExcelExportService excelExportService)
    {
        _eligibleLearnerQueryHandler = eligibleLearnerQueryHandler;
        _learnerConfirmationCommandHandler = learnerConfirmationCommandHandler;
        _excelExportService = excelExportService;
    }

    [HttpPost("search")]
    [ProducesResponseType(typeof(EligibleLearnerSearchResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search(
        [FromBody] SearchEligibleLearnersQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _eligibleLearnerQueryHandler.Handle(query, cancellationToken);
            return Ok(result);
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { code = ex.Code, message = ex.Message });
        }
    }

    [HttpPost("export")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Export(
        [FromBody] SearchEligibleLearnersQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _eligibleLearnerQueryHandler.HandleForExport(query, cancellationToken);
            var content = await _excelExportService.GenerateEligibleLearnersExcelAsync(result.Learners);

            return File(
                content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"EligibleLearners_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { code = ex.Code, message = ex.Message });
        }
    }

    /// <summary>
    /// Forwards the selected learner employees for permanency (confirmation) approval.
    /// </summary>
    [HttpPost("forward-for-approval")]
    [ProducesResponseType(typeof(LearnerConfirmationBatchResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForwardForApproval(
        [FromBody] ForwardLearnersForConfirmationCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _learnerConfirmationCommandHandler.Handle(command, cancellationToken);
            return Ok(result);
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { code = ex.Code, message = ex.Message });
        }
    }

    /// <summary>
    /// Approves (or rejects) the forwarded permanency requests of the selected learner employees.
    /// </summary>
    [HttpPost("approve")]
    [ProducesResponseType(typeof(LearnerConfirmationBatchResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Approve(
        [FromBody] ApproveLearnerConfirmationsCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _learnerConfirmationCommandHandler.Handle(command, cancellationToken);
            return Ok(result);
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { code = ex.Code, message = ex.Message });
        }
    }

    /// <summary>
    /// Returns the permanency requests currently awaiting approval.
    /// </summary>
    [HttpGet("pending-approvals")]
    [ProducesResponseType(typeof(List<PendingLearnerConfirmationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPendingApprovals(CancellationToken cancellationToken)
    {
        var result = await _learnerConfirmationCommandHandler.HandlePending(cancellationToken);
        return Ok(result);
    }
}
