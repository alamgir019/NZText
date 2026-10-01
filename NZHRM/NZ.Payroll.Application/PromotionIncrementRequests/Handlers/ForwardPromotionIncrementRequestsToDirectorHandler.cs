using NZ.HRM.Domain.Entities;
using NZ.Payroll.Application.Interfaces.Repositories;
using NZ.Payroll.Application.PromotionIncrementRequests.Commands;
using NZ.Payroll.Application.PromotionIncrementRequests.DTOs;

namespace NZ.Payroll.Application.PromotionIncrementRequests.Handlers;

public class ForwardPromotionIncrementRequestsToDirectorHandler
{
	private readonly IPromotionIncrementRequestRepository _repository;

	public ForwardPromotionIncrementRequestsToDirectorHandler(IPromotionIncrementRequestRepository repository)
	{
		_repository = repository;
	}

	public async Task<ForwardPromotionIncrementRequestsResultDto> Handle(
		ForwardPromotionIncrementRequestsToDirectorCommand command,
		string currentUser,
		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(currentUser))
			throw new UnauthorizedAccessException("Authenticated user was not found");

		if (command.Requests.Count == 0)
			throw new ArgumentException("At least one promotion increment request is required");

		var items = command.Requests;
		foreach (var item in items)
		{
			item.EmployeeId = item.EmployeeId?.Trim() ?? string.Empty;
			item.ProposedDesignationId = item.ProposedDesignationId?.Trim() ?? string.Empty;
			item.ProposedGradeId = item.ProposedGradeId?.Trim() ?? string.Empty;
		}

		var duplicateEmployeeIds = items
			.GroupBy(item => item.EmployeeId, StringComparer.OrdinalIgnoreCase)
			.Where(group => string.IsNullOrWhiteSpace(group.Key) || group.Count() > 1)
			.Select(group => group.Key)
			.ToList();

		if (duplicateEmployeeIds.Count > 0)
			throw new ArgumentException("Duplicate or empty employee IDs are not allowed in a single request list");

		var employeeIds = items.Select(item => item.EmployeeId).ToList();

		var snapshots = await _repository.GetEmployeeSnapshotsAsync(employeeIds, cancellationToken);
		var snapshotByEmployeeId = snapshots.ToDictionary(
			snapshot => snapshot.EmployeeId,
			StringComparer.OrdinalIgnoreCase);

		var missingEmployeeIds = employeeIds
			.Where(id => !snapshotByEmployeeId.ContainsKey(id))
			.ToList();

		if (missingEmployeeIds.Count > 0)
			throw new KeyNotFoundException($"Employees were not found: {string.Join(", ", missingEmployeeIds)}");

		await EnsureMasterDataExistsAsync(items, cancellationToken);

		var employeesWithOpenRequests = await _repository.GetEmployeeIdsWithOpenRequestsAsync(employeeIds, cancellationToken);
		if (employeesWithOpenRequests.Count > 0)
			throw new InvalidOperationException(
				$"Employees already have a promotion increment request in progress: {string.Join(", ", employeesWithOpenRequests)}");

		var requests = new List<PayPromotionIncrementRequest>();

		foreach (var item in items)
		{
			var snapshot = snapshotByEmployeeId[item.EmployeeId];

			var request = PayPromotionIncrementRequest.Create(
				snapshot.EmployeeId,
				snapshot.DepartmentId,
				snapshot.SectionId,
				snapshot.DesignationId,
				item.ProposedDesignationId,
				snapshot.GradeId,
				item.ProposedGradeId,
				snapshot.GrossSalary ?? 0m,
				item.IncrementPercent,
				item.IncrementAmount,
				item.EffectiveFrom,
				item.Reason,
				currentUser);

			request.ForwardToDirector(currentUser, command.Remarks);
			requests.Add(request);
		}

		await _repository.AddRangeAsync(requests, cancellationToken);

		return new ForwardPromotionIncrementRequestsResultDto
		{
			ForwardedCount = requests.Count,
			ForwardedOn = requests[0].UpdatedOn,
			Items = requests.Select(request => new ForwardedPromotionIncrementRequestDto
			{
				RequestId = request.Id,
				EmployeeId = request.EmployeeId,
				CurrentGrossSalary = request.CurrentGrossSalary,
				NewGrossSalary = request.NewGrossSalary,
				Status = request.Status
			}).ToList()
		};
	}

	private async Task EnsureMasterDataExistsAsync(
		IReadOnlyCollection<PromotionIncrementRequestItem> items,
		CancellationToken cancellationToken)
	{
		var designationIds = items
			.Select(item => item.ProposedDesignationId)
			.Where(id => !string.IsNullOrWhiteSpace(id))
			.Distinct(StringComparer.OrdinalIgnoreCase)
			.ToList();

		var existingDesignationIds = await _repository.GetExistingDesignationIdsAsync(designationIds, cancellationToken);
		var missingDesignationIds = designationIds
			.Where(id => !existingDesignationIds.Contains(id))
			.ToList();

		if (missingDesignationIds.Count > 0)
			throw new KeyNotFoundException($"Designations were not found: {string.Join(", ", missingDesignationIds)}");

		var gradeIds = items
			.Select(item => item.ProposedGradeId)
			.Where(id => !string.IsNullOrWhiteSpace(id))
			.Distinct(StringComparer.OrdinalIgnoreCase)
			.ToList();

		var existingGradeIds = await _repository.GetExistingGradeIdsAsync(gradeIds, cancellationToken);
		var missingGradeIds = gradeIds
			.Where(id => !existingGradeIds.Contains(id))
			.ToList();

		if (missingGradeIds.Count > 0)
			throw new KeyNotFoundException($"Grades were not found: {string.Join(", ", missingGradeIds)}");
	}
}
