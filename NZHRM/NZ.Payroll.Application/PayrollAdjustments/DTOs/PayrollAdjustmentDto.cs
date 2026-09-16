using System;
using NZ.HRM.Domain.Entities;

namespace NZ.Payroll.Application.PayrollAdjustments.DTOs;

public class PayrollAdjustmentDto
{
    public string Id { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string AttendanceMonth { get; set; } = string.Empty;
    public string? CompanyName { get; set; }
    public decimal? OldAmount { get; set; }
    public decimal? NewAmount { get; set; }
    public string? AdjustmentType { get; set; }
    public string? Reason { get; set; }
    public string? Status { get; set; }
    public DateTime? SubmittedOn { get; set; }
}
