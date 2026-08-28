using NZ.HRM.Application.Employees.Queries.GetEmployeeBasicInformation;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.MedicalFitnessChecks.Queries.GetMedicalFitnessHistoryByEmployeeId;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.Shared.Contracts.Leave;

namespace NZ.HRM.Application.Employees.Handlers;

public class GetEmployeeBasicInformationQueryHandler
{
	private readonly IEmployeeMasterRepository _employeeMasterRepository;
	private readonly ILeaveBalanceQuery _leaveBalanceQuery;

	public GetEmployeeBasicInformationQueryHandler(
		IEmployeeMasterRepository employeeMasterRepository,
		ILeaveBalanceQuery leaveBalanceQuery)
	{
		_employeeMasterRepository = employeeMasterRepository;
		_leaveBalanceQuery = leaveBalanceQuery;
	}

	public async Task<List<BasicEmployeeInformationDto>?> Handle(
		GetEmployeeBasicInformationQuery query,
		CancellationToken cancellationToken = default)
	{
		var employee = await _employeeMasterRepository.GetBasicByEmployeeCodeAsync(
			query.SearchText,
			cancellationToken);

		if (employee == null)
			return null;

		var leaveBalances = await _leaveBalanceQuery.GetAllBalancesAsync(
			employee.Select(e => e.Id).ToList(),
			cancellationToken);

		return employee.Select( x => new BasicEmployeeInformationDto
		{
			EmployeeId = x.Id,
			EmployeeCode = x.EmployeeCode,
			EmployeeName = x.EmployeeName,
			EmployeeNameBangla = x.EmployeeNameBangla,
			Leaves = leaveBalances.Where(leave => leave.EmployeeId == x.Id).Select(
				leave => new EmployeeLeaveBalanceDto
				{
					LeaveCode = leave.LeaveCode,
					LeaveName = leave.LeaveName,
					ClosingBalance = leave.ClosingBalance
				}).ToList()
		}).ToList();
	}
}
