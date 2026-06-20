using NZ.HRM.Application.Employees.Queries.GetCompleteEmployee;
using NZ.HRM.Application.Employees.Queries.GetEmployeeConfirmationDate;
using NZ.HRM.Application.Employees.Queries.SearchEmployees;
using NZ.HRM.Application.Interfaces.Repositories;
using NZ.HRM.Application.Model.Employees.DTOs;
using NZ.HRM.Utility.Enum;
using NZ.HRM.Mapping.Employees;

namespace NZ.HRM.Application.Employees.Handlers;

public class CompleteEmployeeQueryHandler
{
    private readonly IEmployeeMasterRepository _employeeMasterRepository;

    public CompleteEmployeeQueryHandler(IEmployeeMasterRepository employeeMasterRepository)
    {
        _employeeMasterRepository = employeeMasterRepository;
    }

    public async Task<EmployeeCompleteDto?> Handle(GetCompleteEmployeeQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);

        if (employee == null)
            return null;

        return employee.MapToEmployeeCompleteDto();
    }

    public async Task<List<EmployeeSearchDto>> Handle(SearchEmployeesQuery query, CancellationToken cancellationToken = default)
    {
        var employees = await _employeeMasterRepository.SearchAsync(query.SearchText, cancellationToken);

        return employees.Select(x => 
        new EmployeeSearchDto() {
            Id = x.Id,
            EmployeeNameEnglish = x.EmployeeNameEnglish,
            EmployeeNameBangla = x.EmployeeNameBangla,
            EnrollmentId = x.EnrollmentId,
            //MobileNumber = x.PersonalInfo?.MobileNumber,
        }).ToList();
    }

    public async Task<DateTime?> Handle(GetEmployeeConfirmationDateQuery query, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeMasterRepository.GetByIdAsync(query.EmployeeId, cancellationToken);
        if (employee == null)
            return null;

        //var probationMonths = employee.EmployeeType == EmployeeType.Worker ? 3 : 6;
        //return query.JoiningDate.AddMonths(probationMonths).Date;
        return null;
    }
}
