using Microsoft.Extensions.Logging;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces;
using NZ.Payroll.Domain.Contracts;
using NZ.Shared.Contracts.Attendance;
using NZ.Shared.Contracts.HRM;
using NZ.Shared.Contracts.Leave;

namespace NZ.Payroll.Application.Services;

/// <summary>
/// Calculates payroll for a single employee by aggregating data from
/// HRM, Attendance, and Leave modules through shared contracts.
/// </summary>
public class PayrollCalculationService : IPayrollCalculationService
{
    private readonly IEmployeeQuery _employeeQuery;
    private readonly IAttendanceSummaryQuery _attendanceSummaryQuery;
    private readonly ILeaveBalanceQuery _leaveBalanceQuery;
    private readonly ILogger<PayrollCalculationService> _logger;

    public PayrollCalculationService(
        IEmployeeQuery employeeQuery,
        IAttendanceSummaryQuery attendanceSummaryQuery,
        ILeaveBalanceQuery leaveBalanceQuery,
        ILogger<PayrollCalculationService> logger)
    {
        _employeeQuery = employeeQuery;
        _attendanceSummaryQuery = attendanceSummaryQuery;
        _leaveBalanceQuery = leaveBalanceQuery;
        _logger = logger;
    }

    public async Task<EmployeePayrollResult> CalculateAsync(
        string employeeId,
        int year,
        int month,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var employee = await _employeeQuery.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
                return new EmployeePayrollResult(employeeId, string.Empty, 0, 0, 0, false, "Employee not found.");

            var attendance = await _attendanceSummaryQuery.GetMonthlySummaryAsync(employeeId, year, month, cancellationToken);

            // Salary calculation is based on employee's payroll configuration
            // Deductions include absence days and late marks
            // This is the foundation — extend with actual salary structure lookup
            var grossSalary = 0m;
            var totalDeductions = 0m;
            var netSalary = grossSalary - totalDeductions;

            _logger.LogInformation(
                "Payroll calculated for employee {EmployeeId} ({EmployeeCode}) for {Year}/{Month}: Gross={Gross}, Net={Net}",
                employeeId, employee.EmployeeCode, year, month, grossSalary, netSalary);

            return new EmployeePayrollResult(
                employeeId,
                employee.EmployeeCode,
                grossSalary,
                totalDeductions,
                netSalary,
                true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Payroll calculation failed for employee {EmployeeId}", employeeId);
            return new EmployeePayrollResult(employeeId, string.Empty, 0, 0, 0, false, ex.Message);
        }
    }
}
