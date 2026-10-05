using Microsoft.EntityFrameworkCore;
using NZ.HRM.Domain.Constants;
using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PromotionIncrementRequests.DTOs;
using NZ.Payroll.Infrastructure.Persistence;

namespace NZ.Payroll.Infrastructure.Repositories;

public class PromotionIncrementRequestRepository : IPromotionIncrementRequestRepository
{
	private readonly PayrollDbContext _context;

	public PromotionIncrementRequestRepository(PayrollDbContext context)
	{
		_context = context;
	}

	public Task<List<PromotionIncrementEmployeeSnapshot>> GetEmployeeSnapshotsAsync(
		IReadOnlyCollection<string> employeeIds,
		CancellationToken cancellationToken = default)
	{
		return (from employee in _context.HrmEmployeeMasters.AsNoTracking()
				where employeeIds.Contains(employee.Id.Trim()) && employee.IsActive
				join employment in _context.HrmEmployeeEmployments.AsNoTracking()
					on employee.Id.Trim() equals employment.EmployeeId into employmentJoin
				from employment in employmentJoin.DefaultIfEmpty()
				join payroll in _context.HrmEmployeePayrolls.AsNoTracking().Where(payroll => payroll.IsActive)
					on employee.Id.Trim() equals payroll.EmployeeId into payrollJoin
				from payroll in payrollJoin.DefaultIfEmpty()
				select new PromotionIncrementEmployeeSnapshot
				{
					EmployeeId = employee.Id.Trim(),
					DepartmentId = employment != null ? employment.DepartmentId : null,
					SectionId = employment != null ? employment.SectionId : null,
					DesignationId = employment != null ? employment.DesignationId : null,
					GradeId = employment != null ? employment.GradeId : null,
					GrossSalary = payroll != null ? payroll.GrossSalary : null
				})
			.ToListAsync(cancellationToken);
	}

	public async Task<HashSet<string>> GetExistingDesignationIdsAsync(
		IReadOnlyCollection<string> designationIds,
		CancellationToken cancellationToken = default)
	{
		var ids = await _context.MstDesignations
			.AsNoTracking()
			.Where(designation => designationIds.Contains(designation.Id.Trim()) && designation.IsActive)
			.Select(designation => designation.Id.Trim())
			.ToListAsync(cancellationToken);

		return ids.ToHashSet(StringComparer.OrdinalIgnoreCase);
	}

	public async Task<HashSet<string>> GetExistingGradeIdsAsync(
		IReadOnlyCollection<string> gradeIds,
		CancellationToken cancellationToken = default)
	{
		var ids = await _context.MstGrades
			.AsNoTracking()
			.Where(grade => gradeIds.Contains(grade.Id.Trim()) && grade.IsActive)
			.Select(grade => grade.Id.Trim())
			.ToListAsync(cancellationToken);

		return ids.ToHashSet(StringComparer.OrdinalIgnoreCase);
	}

	public Task<List<string>> GetEmployeeIdsWithOpenRequestsAsync(
		IReadOnlyCollection<string> employeeIds,
		CancellationToken cancellationToken = default)
	{
		var closedStatuses = PromotionIncrementStatuses.Closed.ToList();

		return _context.PayPromotionIncrementRequests
			.AsNoTracking()
			.Where(request =>
				request.IsActive &&
				employeeIds.Contains(request.EmployeeId) &&
				!closedStatuses.Contains(request.Status))
			.Select(request => request.EmployeeId)
			.Distinct()
			.ToListAsync(cancellationToken);
	}

	public async Task AddRangeAsync(
		IReadOnlyCollection<PayPromotionIncrementRequest> requests,
		CancellationToken cancellationToken = default)
	{
		await _context.PayPromotionIncrementRequests.AddRangeAsync(requests, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}

	public async Task<List<PromotionIncrementRequestListItemDto>> GetListByStatusAsync(
		string status,
		CancellationToken cancellationToken = default)
	{
		var requests = await _context.PayPromotionIncrementRequests
			.AsNoTracking()
			.Where(request => request.IsActive && request.Status == status)
			.ToListAsync(cancellationToken);

		if (requests.Count == 0)
			return new List<PromotionIncrementRequestListItemDto>();

		var requestIds = DistinctIds(requests.Select(request => request.Id));
		var employeeIds = DistinctIds(requests.Select(request => request.EmployeeId));
		var departmentIds = DistinctIds(requests.Select(request => request.DepartmentId));
		var sectionIds = DistinctIds(requests.Select(request => request.SectionId));
		var designationIds = DistinctIds(requests.SelectMany(request =>
			new[] { request.CurrentDesignationId, request.ProposedDesignationId }));
		var gradeIds = DistinctIds(requests.SelectMany(request =>
			new[] { request.CurrentGradeId, request.ProposedGradeId }));

		var employeeById = (await _context.HrmEmployeeMasters
				.AsNoTracking()
				.Where(employee => employeeIds.Contains(employee.Id.Trim()))
				.Select(employee => new { Id = employee.Id.Trim(), employee.EmployeeCode, employee.EmployeeName })
				.ToListAsync(cancellationToken))
			.ToDictionary(employee => employee.Id, StringComparer.OrdinalIgnoreCase);

		var departmentNameById = (await _context.MstDepartments
				.AsNoTracking()
				.Where(department => departmentIds.Contains(department.Id.Trim()))
				.Select(department => new { Id = department.Id.Trim(), department.DepartmentName })
				.ToListAsync(cancellationToken))
			.ToDictionary(department => department.Id, department => department.DepartmentName, StringComparer.OrdinalIgnoreCase);

		var sectionNameById = (await _context.MstSections
				.AsNoTracking()
				.Where(section => sectionIds.Contains(section.Id.Trim()))
				.Select(section => new { Id = section.Id.Trim(), section.SectionName })
				.ToListAsync(cancellationToken))
			.ToDictionary(section => section.Id, section => section.SectionName, StringComparer.OrdinalIgnoreCase);

		var designationNameById = (await _context.MstDesignations
				.AsNoTracking()
				.Where(designation => designationIds.Contains(designation.Id.Trim()))
				.Select(designation => new { Id = designation.Id.Trim(), designation.DesignationName })
				.ToListAsync(cancellationToken))
			.ToDictionary(designation => designation.Id, designation => designation.DesignationName, StringComparer.OrdinalIgnoreCase);

		var gradeById = (await _context.MstGrades
				.AsNoTracking()
				.Where(grade => gradeIds.Contains(grade.Id.Trim()))
				.Select(grade => new { Id = grade.Id.Trim(), grade.GradeName, grade.GradeCode })
				.ToListAsync(cancellationToken))
			.ToDictionary(grade => grade.Id, StringComparer.OrdinalIgnoreCase);

		var trackedActions = new[]
		{
			PromotionIncrementApprovalActions.ApprovedByDirector,
			PromotionIncrementApprovalActions.ReviewedByEmployeeMovementCell
		};

		var actionDates = (await _context.PayPromotionIncrementApprovalHistories
				.AsNoTracking()
				.Where(history =>
					requestIds.Contains(history.PromotionIncrementRequestId.Trim()) &&
					history.IsActive &&
					trackedActions.Contains(history.Action))
				.Select(history => new
				{
					RequestId = history.PromotionIncrementRequestId.Trim(),
					history.Action,
					history.ActionOn
				})
				.ToListAsync(cancellationToken))
			.GroupBy(history => (history.RequestId, history.Action))
			.ToDictionary(group => group.Key, group => group.Max(history => history.ActionOn));

		var approvedIncrementsByEmployee = (await _context.PayIncrementHistories
				.AsNoTracking()
				.Where(increment =>
					employeeIds.Contains(increment.EmployeeId.Trim()) &&
					increment.IsActive &&
					increment.Status == PayIncrementStatuses.Approved &&
					increment.EffectiveDate != null)
				.Select(increment => new
				{
					EmployeeId = increment.EmployeeId.Trim(),
					EffectiveDate = increment.EffectiveDate!.Value,
					increment.IncrementAmount
				})
				.ToListAsync(cancellationToken))
			.GroupBy(increment => increment.EmployeeId, StringComparer.OrdinalIgnoreCase)
			.ToDictionary(
				group => group.Key,
				group => group.OrderByDescending(increment => increment.EffectiveDate).ToList(),
				StringComparer.OrdinalIgnoreCase);

		DateTime? GetActionDate(string requestId, string action) =>
			actionDates.TryGetValue((requestId, action), out var actionOn) ? actionOn : null;

		return requests
			.OrderByDescending(request => GetActionDate(request.Id.Trim(), PromotionIncrementApprovalActions.ApprovedByDirector))
			.ThenByDescending(request => request.CreatedOn)
			.Select(request =>
			{
				var requestId = request.Id.Trim();
				var employeeId = request.EmployeeId.Trim();
				var employee = employeeById.GetValueOrDefault(employeeId);
				var currentGrade = FindById(gradeById, request.CurrentGradeId);
				var proposedGrade = FindById(gradeById, request.ProposedGradeId);
				var lastIncrement = approvedIncrementsByEmployee.GetValueOrDefault(employeeId)?
					.FirstOrDefault(increment => increment.EffectiveDate < request.EffectiveFrom);

				return new PromotionIncrementRequestListItemDto
				{
					RequestId = requestId,
					EmployeeId = request.EmployeeId,
					EmployeeCode = employee?.EmployeeCode ?? string.Empty,
					EmployeeName = employee?.EmployeeName ?? string.Empty,
					Department = FindById(departmentNameById, request.DepartmentId),
					Section = FindById(sectionNameById, request.SectionId),
					CurrentDesignation = FindById(designationNameById, request.CurrentDesignationId),
					ProposedDesignation = FindById(designationNameById, request.ProposedDesignationId),
					CurrentGrade = currentGrade?.GradeName,
					CurrentGradeCode = currentGrade?.GradeCode,
					ProposedGrade = proposedGrade?.GradeName,
					ProposedGradeCode = proposedGrade?.GradeCode,
					LastIncrementDate = lastIncrement?.EffectiveDate,
					LastIncrementAmount = lastIncrement?.IncrementAmount,
					CurrentGrossSalary = request.CurrentGrossSalary,
					IncrementPercent = request.IncrementPercent,
					IncrementAmount = request.IncrementAmount,
					NewGrossSalary = request.NewGrossSalary,
					EffectiveFrom = request.EffectiveFrom,
					Reason = request.Reason,
					DirectorApprovalDate = GetActionDate(requestId, PromotionIncrementApprovalActions.ApprovedByDirector),
					MovementCellReviewDate = GetActionDate(requestId, PromotionIncrementApprovalActions.ReviewedByEmployeeMovementCell),
					Status = request.Status
				};
			})
			.ToList();
	}

	private static List<string> DistinctIds(IEnumerable<string?> ids)
	{
		return ids
			.Where(id => !string.IsNullOrWhiteSpace(id))
			.Select(id => id!.Trim())
			.Distinct(StringComparer.OrdinalIgnoreCase)
			.ToList();
	}

	private static TValue? FindById<TValue>(IReadOnlyDictionary<string, TValue> map, string? id)
		where TValue : class
	{
		return string.IsNullOrWhiteSpace(id) ? null : map.GetValueOrDefault(id.Trim());
	}

	public Task<List<PayPromotionIncrementRequest>> GetByIdsAsync(
		IReadOnlyCollection<string> requestIds,
		CancellationToken cancellationToken = default)
	{
		return _context.PayPromotionIncrementRequests
			.Where(request => requestIds.Contains(request.Id.Trim()) && request.IsActive)
			.ToListAsync(cancellationToken);
	}

	public async Task SaveTransitionsAsync(
		IReadOnlyCollection<PayPromotionIncrementRequest> requests,
		IReadOnlyCollection<PayPromotionIncrementApprovalHistory> histories,
		CancellationToken cancellationToken = default)
	{
		// Ids are generated client-side, so new history rows must be added explicitly;
		// if only discovered via the navigation, EF would treat them as existing rows.
		await _context.PayPromotionIncrementApprovalHistories.AddRangeAsync(histories, cancellationToken);

		foreach (var request in requests)
		{
			if (_context.Entry(request).State == EntityState.Detached)
				_context.PayPromotionIncrementRequests.Update(request);
		}

		await _context.SaveChangesAsync(cancellationToken);
	}

	public Task<bool> ExistsAsync(
		string requestId,
		CancellationToken cancellationToken = default)
	{
		return _context.PayPromotionIncrementRequests.AnyAsync(
			request => request.Id.Trim() == requestId && request.IsActive,
			cancellationToken);
	}

	public Task<List<PayPromotionIncrementApprovalHistory>> GetApprovalHistoryAsync(
		string requestId,
		CancellationToken cancellationToken = default)
	{
		return _context.PayPromotionIncrementApprovalHistories
			.AsNoTracking()
			.Where(history => history.PromotionIncrementRequestId == requestId && history.IsActive)
			.OrderBy(history => history.ActionOn)
			.ThenBy(history => history.StepNo)
			.ToListAsync(cancellationToken);
	}

	public async Task ForwardBatchAsync(
		IReadOnlyCollection<PayPromotionIncrementRequest> requests,
		IReadOnlyCollection<PayPromotionIncrementApprovalHistory> histories,
		CancellationToken cancellationToken = default)
	{
		await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

		try
		{
			await _context.PayPromotionIncrementRequests.AddRangeAsync(requests, cancellationToken);
			await _context.PayPromotionIncrementApprovalHistories.AddRangeAsync(histories, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			await transaction.CommitAsync(cancellationToken);
		}
		catch
		{
			await transaction.RollbackAsync(cancellationToken);
			throw;
		}
	}
}
