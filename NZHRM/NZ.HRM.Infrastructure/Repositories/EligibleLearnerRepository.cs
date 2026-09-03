using Microsoft.EntityFrameworkCore;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.LearnerAdjustments.Queries;
using NZ.HRM.Application.Model.LearnerAdjustments.DTOs;
using NZ.HRM.Domain.Services;
using NZ.HRM.Infrastructure.Persistence;

namespace NZ.HRM.Infrastructure.Repositories;

public class EligibleLearnerRepository : IEligibleLearnerRepository
{
    private readonly ApplicationDbContext _context;

    public EligibleLearnerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(List<EligibleLearnerDto> Learners, int TotalRecords, decimal TotalAdjustmentAmount)>
        GetEligibleLearnersAsync(EligibleLearnerFilter filter, CancellationToken cancellationToken = default)
    {
        // Standard worker gross salary is maintained in salary master data (grade of the Worker designation).
        var standardGrossSalary = await _context.MstDesignations
            .AsNoTracking()
            .Where(d => d.DesignationName.ToLower() == ProbationAdjustmentPolicy.StandardWorkerDesignationName.ToLower()
                        && d.Grade != null)
            .Select(d => (decimal?)d.Grade!.MinimumSalary)
            .FirstOrDefaultAsync(cancellationToken);

        if (standardGrossSalary is null || standardGrossSalary.Value <= 0m)
            return (new List<EligibleLearnerDto>(), 0, 0m);

        var learnerDesignation = ProbationAdjustmentPolicy.LearnerDesignationName.ToLower();
        var activeStatus = ProbationAdjustmentPolicy.ActiveStatus.ToLower();

        var query =
            from employee in _context.HrmEmployeeMasters.AsNoTracking()
            join employment in _context.HrmEmployeeEmployments.AsNoTracking()
                on employee.Id equals employment.EmployeeId
            join payroll in _context.HrmEmployeePayrolls.AsNoTracking()
                on employee.Id equals payroll.EmployeeId
            where employee.IsActive
                  && employee.Status.ToLower() == activeStatus
                  && employment.Designation != null
                  && employment.Designation.DesignationName.ToLower() == learnerDesignation
                  && employment.JoiningDate != null
                  && employment.JoiningDate >= filter.JoiningDateFrom
                  && employment.JoiningDate <= filter.JoiningDateTo
                  && payroll.GrossSalary != null
                  && payroll.GrossSalary > 0
            select new
            {
                EmployeeId = employee.EmployeeCode,
                employee.EmployeeName,
                DepartmentName = employment.Department != null ? employment.Department.DepartmentName : string.Empty,
                Designation = employment.Designation!.DesignationName,
                DateOfJoining = employment.JoiningDate!.Value,
                CurrentGrossSalary = payroll.GrossSalary!.Value
            };

        // Probation completion uses DateOnly.AddMonths which is not translatable to SQL,
        // so it is evaluated after the database has narrowed the candidate set.
        var candidates = await query
            .OrderBy(x => x.DateOfJoining)
            .ThenBy(x => x.EmployeeId)
            .ToListAsync(cancellationToken);

        var eligible = candidates
            .Select(x => new EligibleLearnerDto
            {
                EmployeeId = x.EmployeeId,
                EmployeeName = x.EmployeeName,
                DepartmentName = x.DepartmentName,
                Designation = x.Designation,
                DateOfJoining = x.DateOfJoining,
                ProbationCompletedOn = ProbationAdjustmentPolicy
                    .CalculateProbationCompletedOn(x.DateOfJoining, filter.ProbationPeriodMonths),
                CurrentGrossSalary = decimal.Round(x.CurrentGrossSalary, 2, MidpointRounding.AwayFromZero),
                StandardGrossSalary = decimal.Round(standardGrossSalary.Value, 2, MidpointRounding.AwayFromZero),
                AdjustmentAmount = ProbationAdjustmentPolicy
                    .CalculateAdjustmentAmount(standardGrossSalary.Value, x.CurrentGrossSalary)
            })
            .Where(x => x.ProbationCompletedOn <= filter.BusinessDate)
            .OrderBy(x => x.DateOfJoining)
            .ThenBy(x => x.EmployeeId)
            .ToList();

        var totalRecords = eligible.Count;
        var totalAdjustmentAmount = eligible.Sum(x => x.AdjustmentAmount);

        var page = filter.NoPaging
            ? eligible
            : eligible
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

        return (page, totalRecords, totalAdjustmentAmount);
    }
}
