using Microsoft.Extensions.Logging;
using NZ.Payroll.Application.Interfaces;
using NZ.Payroll.Domain.Contracts;

namespace NZ.Payroll.Application.Services;

public class PayrollProcessingService : IPayrollProcessingService
{
    private readonly IPayrollCalculationService _calculationService;
    private readonly ILogger<PayrollProcessingService> _logger;

    public PayrollProcessingService(
        IPayrollCalculationService calculationService,
        ILogger<PayrollProcessingService> logger)
    {
        _calculationService = calculationService;
        _logger = logger;
    }

    public async Task<IReadOnlyList<EmployeePayrollResult>> ProcessAsync(
        PayrollProcessRequest request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Starting payroll processing for group {GroupId}, period {Year}/{Month}, {Count} employees.",
            request.PayrollGroupId, request.Year, request.Month, request.EmployeeIds.Count);

        var tasks = request.EmployeeIds
            .Select(id => _calculationService.CalculateAsync(id, request.Year, request.Month, cancellationToken));

        var results = await Task.WhenAll(tasks);

        var successCount = results.Count(r => r.IsSuccess);
        _logger.LogInformation(
            "Payroll processing complete: {Success}/{Total} employees processed successfully.",
            successCount, results.Length);

        return results;
    }
}
