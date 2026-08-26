using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.Shared.Contracts.Leave;

namespace NZ.HRM.Application.Employees.Queries.GetEmployeeBasicInformation;

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

	public async Task<BasicEmployeeInformationDto?> Handle(
		GetEmployeeBasicInformationQuery query,
		CancellationToken cancellationToken = default)
	{
		var employee = await _employeeMasterRepository.GetBasicByEmployeeCodeAsync(
			query.EmployeeCode,
			cancellationToken);

		if (employee == null)
			return null;

		var leaveBalances = await _leaveBalanceQuery.GetAllBalancesAsync(
			employee.Id,
			cancellationToken);

		return new BasicEmployeeInformationDto
		{
			EmployeeCode = employee.EmployeeCode,
			EmployeeName = employee.EmployeeName,
			EmployeeNameBangla = employee.EmployeeNameBangla,
			Leaves = leaveBalances.Select(leave => new EmployeeLeaveBalanceDto
			{
				LeaveCode = leave.LeaveCode,
				LeaveName = leave.LeaveName,
				ClosingBalance = leave.ClosingBalance
			}).ToList()
		};
	}
}
